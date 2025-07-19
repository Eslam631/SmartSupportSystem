namespace Domain.Exceptions
{
   public sealed class UnauthorizeException(string message="Invalid Email Or Password"):Exception(message)
    {
    }
}
