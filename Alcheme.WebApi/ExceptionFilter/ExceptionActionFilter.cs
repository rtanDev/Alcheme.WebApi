using System;
using Alcheme.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Alcheme.WebApo.ExceptionFilter
{
    public class ExceptionActionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionActionFilter> _logger;

        public ExceptionActionFilter(ILogger<ExceptionActionFilter> logger)
        {
            this._logger = logger;
        }

        #region override

        public override void OnException(ExceptionContext context)
        {
            ApiError apiError = null;
            if (context.Exception is ApiException)
            {
                var _exception = context.Exception as ApiException;
                context.Exception = null;
                apiError = new ApiError(_exception.Message);
                context.HttpContext.Response.StatusCode = _exception.StatusCode;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;

                // handle logging here
                _logger.LogInformation(string.Format("{0} - {1} Unauthorized Access.", 401, DateTime.Now.ToShortDateString()));
            }
            else
            {
                // Unhandled errors
#if !DEBUG
                var msg = "An unhandled error occurred.";                
                string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg);
                apiError.Detail = stack;

                context.HttpContext.Response.StatusCode = 500;

                // handle logging here
                _logger.LogError(msg);
            }

            // always return a JSON result
            context.Result = new JsonResult(apiError);

            base.OnException(context);
        }

        #endregion
    }
}