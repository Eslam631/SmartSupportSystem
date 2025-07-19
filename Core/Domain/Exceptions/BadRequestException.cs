namespace Domain.Exceptions
{
    public sealed class BadRequestException(List<string> Errors):Exception("validation Faild")
    {
        public List<string> Errors { get; } = Errors;
    
    }
}
