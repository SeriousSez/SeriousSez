using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SeriousSez.Api.Security
{
    public class AdminAuthorizationAttribute : AuthorizeAttribute
    {
        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //    if (AuthorizeRequest(actionContext))
        //    {
        //        return;
        //    }

        //    HandleUnauthorizedRequest(actionContext);
        //}

        //protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        //{
        //    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.TemporaryRedirect);
        //    actionContext.Response.Headers.Add("Location", "~/unauthorized");
        //}

        private bool AuthorizeRequest(HttpActionContext actionContext)
        {
            var methodName = actionContext.Request.Method.Method;
            switch (actionContext.ControllerContext.ControllerDescriptor.ControllerName.ToLowerInvariant())
            {
                case "dashboard":
                    return AuthorizeMethod(methodName);
                default:
                    return false;
            }
        }

        private bool AuthorizeMethod(string method)
        {
            switch (method)
            {
                case "":
                    return true;
                default:
                    return false;
            }
        }
    }
}
