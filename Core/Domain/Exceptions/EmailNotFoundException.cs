namespace Domain.Exceptions
{
    public sealed class EmailNotFoundException(string email) : NotFoundException($"The email '{email}' was not found.")
    {
    }

}

