namespace Shared.DepartmentDto
{
    public record DepartmentResponse
    {
        public  Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
