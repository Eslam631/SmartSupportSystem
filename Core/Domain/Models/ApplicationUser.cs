using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string RoleId { get; set; } = default!;

        public IdentityRole Role { get; set; } = default!;

        public List<RefreshToken> RefreshTokens { get; set; } = [];

    }
}
