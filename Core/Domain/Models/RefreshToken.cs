using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    [Owned]
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireOn { get; set; }
        public DateTime CreateOn { get; set; }= DateTime.UtcNow;
        public DateTime? RevokeOn { get; set; } 

        public bool IsExpired=> ExpireOn <= DateTime.UtcNow;
        public bool IsActive => RevokeOn is null && !IsExpired;


    }
}
