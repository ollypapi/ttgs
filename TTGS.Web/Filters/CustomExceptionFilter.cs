using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Net;

namespace TTGS.Web.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception, "System Error");

            var jsonResult = new JsonResult(context.Exception);
            jsonResult.StatusCode = (int)HttpStatusCode.InternalServerError;
            jsonResult.ExecuteResult(context);
            context.ExceptionHandled = true;
            context.Result = jsonResult;
        }
    }
}
