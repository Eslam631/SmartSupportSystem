using System.ComponentModel.DataAnnotations;

namespace Shared.AuthDto
{
   public record RegisterRequest
    {
        [EmailAddress]
        public string Email { get; init; } = string.Empty;

        public string FristName { get; init; } = string.Empty;
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; init; } = string.Empty;

       
    }
}
