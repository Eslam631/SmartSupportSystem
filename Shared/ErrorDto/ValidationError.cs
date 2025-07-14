namespace Shared.ErrorDto
{
    public record ValidationError
    {
        public string Faild { get; set; } = default!;
      public IEnumerable<string> Errors { get; set; } = [];
    }
}
