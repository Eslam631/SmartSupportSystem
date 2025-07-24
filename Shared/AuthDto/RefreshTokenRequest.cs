namespace Shared.AuthDto
{
    public record RefreshTokenRequest
    {
        public string Token { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
    }
}
