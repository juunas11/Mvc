// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SecurityWebSite.Controllers
{
    public class ChallengeController : ControllerBase
    {
        public IActionResult AutomaticBehavior()
        {
            return Challenge();
        }

        public IActionResult UnauthorizedBehavior()
        {
            return Challenge(ChallengeBehavior.Unauthorized);
        }

        public IActionResult ForbiddenBehavior()
        {
            return Challenge(ChallengeBehavior.Forbidden);
        }
    }
}
