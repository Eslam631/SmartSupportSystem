namespace Domain.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }=new Guid();
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
