namespace Shared.AuthDto
{
    public record UserDto
    {
        public string Email { get; init; } = string.Empty;
        public string Token { get; init; } = string.Empty;
        public DateTime ExpireIn { get; init; } 
    }
}
