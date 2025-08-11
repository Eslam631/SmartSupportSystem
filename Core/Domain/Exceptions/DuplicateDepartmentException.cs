namespace Domain.Exceptions
{
    public sealed class DuplicateDepartmentException(string message) : Exception(message)
    {
    }
}
