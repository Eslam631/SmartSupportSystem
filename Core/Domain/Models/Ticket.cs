namespace Domain.Models
{
    public class Ticket :BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Status Status { get; set; } // e.g., Open, In Progress, Closed
        public Department Department { get; set; } = default!;
       public Guid DepartmentId { get; set; } = default!;

        public ApplicationUser? SuperAgent { get; set; } = default!;
        public string? AgentId { get; set; } // User ID of the person assigned to the ticket
        
        


    }
}
