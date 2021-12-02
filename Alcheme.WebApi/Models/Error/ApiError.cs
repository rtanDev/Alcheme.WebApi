using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Alcheme.WebApi.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public bool isError { get; set; }
        public string Detail { get; set; }

        public ApiError(string message)
        {
            this.Message = message;
            isError = true;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            this.isError = true;
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                this.Message = "Please correct corresponding errors and try again.";
            }
        }
    }
}
