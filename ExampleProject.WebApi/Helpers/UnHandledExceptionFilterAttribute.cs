using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ExampleProject.WebApi.Helpers
{
    public class UnHandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if(actionExecutedContext.Exception is ArgumentException)
            {
                actionExecutedContext.Response = CreateErrorResponse(HttpStatusCode.ExpectationFailed, actionExecutedContext.Exception.Message);
            }
            else
            {
                actionExecutedContext.Response = CreateErrorResponse(HttpStatusCode.BadRequest, actionExecutedContext.Exception.Message);
            }
        }
        private HttpResponseMessage CreateErrorResponse(HttpStatusCode statusCode, string message)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message),
                ReasonPhrase = message
            };
        }
    }
}