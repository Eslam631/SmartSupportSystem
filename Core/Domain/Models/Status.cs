namespace Domain.Models
{
    public enum Status
    {
        Open,          // Ticket is newly created and not yet addressed
        InProgress,    // Ticket is currently being worked on
        Resolved,      // Issue has been addressed but not yet closed
        Closed,        // Ticket is completed and closed

    }
}
