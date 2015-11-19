using MVC_Using_Angular.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace MVC_Using_Angular.Controllers
{
    public class AuthenticateController : ApiController
    {
        [HttpPost]
        public string Post(Credential credential)
        {
            if (credential.username == "admin" && credential.password == "123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = Authorization.GetBytes("anyoldrandomtext");
                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]                     
                    {                     
                     new Claim( ClaimTypes.UserData,"IsValid", ClaimValueTypes.String, "(local)" )                     
                     }),
                    TokenIssuerName = "self",
                    AppliesToAddress = "https://www.mywebsite.com",
                    Lifetime = new Lifetime(now, now.AddMinutes(60)),
                    SigningCredentials = new SigningCredentials(new InMemorySymmetricSecurityKey(securityKey),
                      "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                      "http://www.w3.org/2001/04/xmlenc#sha256"),
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return tokenString;
            }
            else
            {
                return string.Empty;
            }
        }

        


    }

    public class Authorization
    {
        public static byte[] GetBytes(string input)
        {
            var bytes = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;

        }
    }
}
