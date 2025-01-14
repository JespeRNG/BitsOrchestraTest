using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext filterContext)
    {
        if (filterContext.ExceptionHandled)
        {
            return;
        }

        var exception = filterContext.Exception;

        filterContext.HttpContext.Response.Clear();
        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        filterContext.HttpContext.Response.ContentType = "application/json";

        var response = new
        {
            error = true,
            message = "An unexpected error occurred.",
            details = exception.Message
        };

        var jsonResponse = JsonConvert.SerializeObject(response);

        filterContext.Result = new ContentResult
        {
            Content = jsonResponse,
            ContentType = "application/json"
        };

        filterContext.ExceptionHandled = true;
    }
}
