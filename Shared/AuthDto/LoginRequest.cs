using System.ComponentModel.DataAnnotations;

namespace Shared.AuthDto
{
    public record LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
      
    }
}
