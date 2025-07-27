namespace Shared.DepartmentDto
{
    public record DepartmentDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; } 
        public DateTime? UpdatedAt { get; set; }
        // Additional properties can be added as needed
    }
}
