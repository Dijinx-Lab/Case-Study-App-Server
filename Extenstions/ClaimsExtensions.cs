using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CaseStudyAppServer.Extenstions
{
    public static class ClaimsExtensions
    {
        public static string? GetUserEmail(this ClaimsPrincipal user)
        {
            var emailClaim = user.Claims.FirstOrDefault(
                x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", StringComparison.OrdinalIgnoreCase)
            );
            return emailClaim?.Value;
        }
    }
}