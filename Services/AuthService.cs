using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Services.settingOption;
using Shared.AuthDto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Services
{
    public class AuthService(RoleManager<IdentityRole> _roleManager ,UserManager<ApplicationUser> _userManager,IOptions<JwtSettingOption> options) : IAuthService
    {
        private readonly JwtSettingOption _Options = options.Value;
        private readonly int _ExpirationInRefreshTokenDays = 15;
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
                if(existingEmail is not null)
                throw new DuplicateEmailException("This is Email Existing");
            
            var role = await _roleManager.FindByNameAsync("Customer");
            if (role is null)
                throw new RoleNotFound("Customer");
            var User = new ApplicationUser
                {
                    UserName = request.Email.Split('@')[0],
                    Email = request.Email,
                    FirstName = request.FristName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    RoleId = role.Id ,
                };

            var result = await _userManager.CreateAsync(User, request.Password);

            if (!result.Succeeded) {
                var Errors =  result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
            var Token = await createTokenAsync(User);
         
           
            return new RegisterResponse
            {
                Email = User.Email,
                token=Token,
                 FristName= User.FirstName,
                 LastName = User.LastName,
                 RoleName = role.Name!,
                   ExpiresIn=_Options.ExpirationInMinutes,
                 

            };

        }
        public async Task<RegisterResponse> CreateSupportAgent(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail is not null)
                throw new DuplicateEmailException("This is Email Existing");

            var role = await _roleManager.FindByNameAsync("Support Agent");
            if (role is null)
                throw new RoleNotFound("Support Agent");
            var User = new ApplicationUser
            {
                UserName = request.Email.Split('@')[0],
                Email = request.Email,
                FirstName = request.FristName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                RoleId = role.Id,

            };

            var result = await _userManager.CreateAsync(User, request.Password);

            if (!result.Succeeded)
            {
                var Errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
            var Token = await createTokenAsync(User);
           
          
            return new RegisterResponse
            {
                Email = User.Email,
                token = Token,
                FristName = User.FirstName,
                LastName = User.LastName,
                RoleName = role.Name!,
                ExpiresIn = _Options.ExpirationInMinutes,
              

            };
        }

        public async Task<UserDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new EmailNotFoundException(request.Email);

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                throw new UnauthorizeException();
            }
            var token = await createTokenAsync(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpireOn = DateTime.UtcNow.AddDays(_ExpirationInRefreshTokenDays)
            });

            await _userManager.UpdateAsync(user);

            return new UserDto
            {
                Email = user.Email!,
                Token = token,
                ExpireIn = DateTime.UtcNow.AddMinutes(_Options.ExpirationInMinutes),
                RefreshToken = refreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(_ExpirationInRefreshTokenDays)

            };
        }
        public async Task<UserDto> GetRefreshTokenAsync(string Token, string refreshToken, CancellationToken cancellationToken = default)
        {
            var userId=ValidateToken(Token);
            //f65192c4 - 947a - 422c - 8c44 - 64ac4e9efa2d
            if (userId is null)
                throw new UnauthorizeException();

            var user = await _userManager.FindByIdAsync( userId);
            if (user is null)
                throw new EmailNotFoundException(userId);


            var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken&&rt.IsActive);

            if (userRefreshToken is null)
                throw new UnauthorizeException("Invalid refresh token");

            userRefreshToken.RevokeOn = DateTime.UtcNow;

            var newToken = await createTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpireOn = DateTime.UtcNow.AddDays(_ExpirationInRefreshTokenDays)
            });

            await _userManager.UpdateAsync(user);

            return new UserDto
            {
                Email = user.Email!,
                Token = newToken,
                ExpireIn = DateTime.UtcNow.AddMinutes(_Options.ExpirationInMinutes),
                RefreshToken = newRefreshToken,
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(_ExpirationInRefreshTokenDays)

            };

        }
        public string? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Options.SecretKey));
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero // Optional: Set clock skew to zero for immediate expiration
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken; 

                return jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;



            }
            catch (Exception)
            {
                return null;

            }

            

        }
        private async Task<string> createTokenAsync(ApplicationUser user)
        {
            
            var Claims= new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Email, user.Email!),
                new (ClaimTypes.Name, user.UserName!),
            };


          var Role=await  _roleManager.FindByIdAsync(user.RoleId);

            if (Role is not null)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role.Name! ));
            }

           var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Options.SecretKey));
          var cred=new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer:_Options.Issuer,
                audience:_Options.Audience,
                claims:Claims,
                expires:DateTime.UtcNow.AddMinutes(_Options.ExpirationInMinutes),
                signingCredentials:cred);
         

            var tokenHandler = new JwtSecurityTokenHandler();

            // Placeholder for actual token generation logic
            return tokenHandler.WriteToken(token);
        }

       private static string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }

    }
}
