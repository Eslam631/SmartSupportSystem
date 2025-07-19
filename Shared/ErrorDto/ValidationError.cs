namespace Shared.ErrorDto
{
    public record ValidationError
    {
        public string Feild { get; set; } = default!;
      public IEnumerable<string> Errors { get; set; } = [];
    }
}
