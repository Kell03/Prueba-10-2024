using Microsoft.AspNetCore.Authentication;
using System.Text;

namespace ApiPractice.Auth
{
    public class CustomAuthenticationSchemeOptions: AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "BasicAuthentication";
        public const string AuthorizationHeaderName = "Authorization";
    }
}
