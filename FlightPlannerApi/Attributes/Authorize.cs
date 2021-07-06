using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlightPlannerApi.Attributes
{
    public class Authorize : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers
                    .Authorization.Parameter;
                var decodeAuthToken = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(authToken));

                var arrUserNameAndPassword = decodeAuthToken.Split(':');

                if (IsAuthorizedUser(arrUserNameAndPassword[0], arrUserNameAndPassword[1]))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(arrUserNameAndPassword[0]), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request
                        .CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request
                    .CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        private bool IsAuthorizedUser(string username, string password)
        {
            return username == "codelex-admin" && password == "Password123";
        }
    }
}