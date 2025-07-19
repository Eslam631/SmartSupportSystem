namespace Shared.ErrorDto
{
    public record ErrorToReturn
    {
        public int status { get; set; }
        public string ErrorMassage { get; set; } = default!;
        public List<string>? Errors { get; set; } 
    }
}
