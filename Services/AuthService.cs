using Domain.Exceptions;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;
using Shared.AuthDto;

namespace Services
{
    public class AuthService(RoleManager<IdentityRole> _roleManager ,UserManager<ApplicationUser> _userManager) : IAuthService
    {
        public async Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
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

            return new UserDto
            {
                Email = User.Email,
                Token="Token",
                ExpireIn= DateTime.UtcNow.AddHours(5)

            };

        }
        public async Task<UserDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                throw new EmailNotFoundException("User not found");

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result)
            {
                throw new UnauthorizeException();
            }


            return new UserDto
            {
                Email = user.Email!,
                Token = "Token",
                ExpireIn = DateTime.UtcNow.AddHours(5),

            };
        }

    }
}
