namespace Shared.AuthDto
{
    public record RegisterResponse
    {
       
        public string Email { get; init; } = string.Empty;

        public string FristName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
      
        public string RoleName { get; init; } = string.Empty;


    }
}
