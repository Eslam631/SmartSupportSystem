using System.Net;

namespace Shared.ErrorDto
{
    public record ValidationErrorToReturn
    {
        public int statusCode { get;  }=(int) HttpStatusCode.BadRequest;
        public string message { get; set; } = default!;
        public IEnumerable<ValidationError>  validationErrors{ get; set; } = [];
    }
}
