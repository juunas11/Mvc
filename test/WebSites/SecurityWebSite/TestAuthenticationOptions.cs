using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace SecurityWebSite
{
    /// <summary>
    /// Options for the <see cref="TestAuthenticationHandler"/>.
    /// </summary>
    public class TestAuthenticationOptions : AuthenticationSchemeOptions
    {
        public TestAuthenticationOptions()
        {
        }

        public PathString LoginPath { get; set; }
        
        public PathString LogoutPath { get; set; }
        
        public PathString AccessDeniedPath { get; set; }
    }
}
