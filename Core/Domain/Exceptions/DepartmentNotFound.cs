namespace Domain.Exceptions
{
   public sealed class DepartmentNotFound(Guid department):NotFoundException($"This Is Department ={department} Not Found")
    {
    }
}
