using MVC_Using_Angular.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Security.Tokens;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;


namespace MVC_Using_Angular.App_Start
{
    public class AuthorizeApi : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
           
            string token;

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            else
            {
                if (!TryRetrieveToken(actionContext.Request, out token))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var securityKey = MVC_Using_Angular.Controllers.Authorization.GetBytes("anyoldrandomtext");
                    var validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = "https://www.mywebsite.com",
                        IssuerSigningToken = new BinarySecretSecurityToken(securityKey),
                        ValidIssuer = "self"
                    };
                    try
                    {
                        SecurityToken securityToken;
                        var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                        var userData = principal.Claims.FirstOrDefault();
                        if (userData != null)
                        {
                            var isValid = userData.Value;
                            if (isValid != "IsValid")
                            {
                                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                            }
                        }
                        else
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        }
                    }
                    catch (Exception exception)
                    {
                        // log the exception details here                     
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }

                }
            }
        }

        private bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authorizationHeaders;

            if (!request.Headers.TryGetValues("Authorization", out authorizationHeaders) ||
            authorizationHeaders.Count() > 1)
            {
                return false;
            }

            var bearerToken = authorizationHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            token = token.Substring(1, token.Length - 2);
            return true;
        }
    }
}