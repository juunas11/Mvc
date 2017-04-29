// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SecurityWebSite
{
    /// <summary>
    /// Authentication handler for functional tests.
    /// Considers user authenticated if a request header
    /// with key "authenticated" is found.
    /// Allows easy testing of behavior depending
    /// on user being authenticated.
    /// </summary>
    public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationOptions>
    {
        public TestAuthenticationHandler(
            IOptionsSnapshot<TestAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("authenticated"))
            {
                return Task.FromResult(AuthenticateResult.None());
            }

            // Create a basic principal so the user is authenticated
            var claimsIdentity = new ClaimsIdentity(Scheme.Name);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationTicket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
        }

        // Replicates behavior of CookieAuthenticationHandler
        // Unauthorized -> redirect to login path
        // Forbidden -> redirect to access denied path

        protected override Task HandleUnauthorizedAsync(ChallengeContext context)
        {
            context.Response.Redirect(BuildRedirectUri(Options.LoginPath));
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync(ChallengeContext context)
        {
            context.Response.Redirect(BuildRedirectUri(Options.AccessDeniedPath));
            return Task.CompletedTask;
        }
    }
}
