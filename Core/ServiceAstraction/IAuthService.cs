using Shared.AuthDto;

namespace ServiceAbstraction
{
    public interface IAuthService
    {
        public Task<UserDto> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
        public Task<UserDto> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    }
}
