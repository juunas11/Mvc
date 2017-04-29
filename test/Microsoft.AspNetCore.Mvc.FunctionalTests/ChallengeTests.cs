// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SecurityWebSite;
using Xunit;

namespace Microsoft.AspNetCore.Mvc.FunctionalTests
{
    public class ChallengeTests : IClassFixture<MvcTestFixture<Startup>>
    {
        public ChallengeTests(MvcTestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task AutomaticBehaviorRedirectsUnauthenticatedUserToLogin()
        {
            // Arrange & Act
            var response = await Client.GetAsync("http://localhost/Challenge/AutomaticBehavior");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Home/Login", response.Headers.Location.AbsolutePath, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task AutomaticBehaviorRedirectsAuthenticatedUserToAccessDenied()
        {
            // Arrange
            var req = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Challenge/AutomaticBehavior");
            req.Headers.Add("authenticated", "true");

            // Act
            var response = await Client.SendAsync(req);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Home/AccessDenied", response.Headers.Location.AbsolutePath, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task UnauthorizedBehaviorRedirectsUnauthenticatedUserToLogin()
        {
            // Arrange & Act
            var response = await Client.GetAsync("http://localhost/Challenge/UnauthorizedBehavior");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Home/Login", response.Headers.Location.AbsolutePath, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task UnauthorizedBehaviorRedirectsAuthenticatedUserToLogin()
        {
            // Arrange
            var req = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Challenge/UnauthorizedBehavior");
            req.Headers.Add("authenticated", "true");

            // Act
            var response = await Client.SendAsync(req);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Home/Login", response.Headers.Location.AbsolutePath, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ForbiddenBehaviorRedirectsUnauthenticatedUserToAccessDenied()
        {
            // Arrange & Act
            var response = await Client.GetAsync("http://localhost/Challenge/ForbiddenBehavior");

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Home/AccessDenied", response.Headers.Location.AbsolutePath, StringComparer.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task ForbiddenBehaviorRedirectsAuthenticatedUserToAccessDenied()
        {
            // Arrange
            var req = new HttpRequestMessage(HttpMethod.Get, "http://localhost/Challenge/ForbiddenBehavior");
            req.Headers.Add("authenticated", "true");

            // Act
            var response = await Client.SendAsync(req);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal("/Home/AccessDenied", response.Headers.Location.AbsolutePath, StringComparer.OrdinalIgnoreCase);
        }

    }
}
