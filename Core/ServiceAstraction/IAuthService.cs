using Shared.AuthDto;

namespace ServiceAbstraction
{
    public interface IAuthService
    {
        public Task<RegisterResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        public Task<RegisterResponse> CreateSupportAgent(RegisterRequest request, CancellationToken cancellationToken = default);
        public Task<UserDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

        public Task<UserDto> GetRefreshTokenAsync(string Token,string refreshToken, CancellationToken cancellationToken = default);
        public string? ValidateToken(string token);

    }
}
