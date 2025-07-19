namespace Domain.Exceptions
{
    public sealed class RoleNotFound(string role):NotFoundException($"this role ={role} is not found ")
    {
    }
}
