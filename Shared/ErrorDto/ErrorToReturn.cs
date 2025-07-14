namespace Shared.ErrorDto
{
    public record ErrorToReturn
    {
        public int status { get; set; }
        public string Error { get; set; } = default!;
    }
}
