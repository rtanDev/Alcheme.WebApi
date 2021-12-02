using Alcheme.WebApo.ExceptionFilter;
using Microsoft.AspNetCore.Mvc;

namespace Alcheme.WebApi.Controllers
{
    [Produces("application/json")]
    [TypeFilter(typeof(ExceptionActionFilter))]
    public class ControllerBase : Controller
    {       
    }
}