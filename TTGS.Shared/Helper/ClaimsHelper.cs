using TTGS.Shared.Constants;
using System;
using System.Linq;
using System.Security.Claims;

namespace TTGS.Shared.Helper
{
    public static class ClaimsHelper
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetRoleName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        }

        public static Guid GetCompanyId(this ClaimsPrincipal claimsPrincipal)
        {
            var companyId = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == CustomClaimConstants.CompanyId)?.Value;
            Guid.TryParse(companyId, out Guid newCompanyId);
            return newCompanyId;
        }

        public static string GetCompanyName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == CustomClaimConstants.CompanyName)?.Value;
        }

        public static string GetContactPersonName(this ClaimsPrincipal claimsPrincipal)
        {
            var contactPersonName = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == CustomClaimConstants.ContactPerson)?.Value;
            return contactPersonName;
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }

        public static bool CompanyIsApproved(this ClaimsPrincipal claimsPrincipal)
        {
            var value = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == CustomClaimConstants.IsApproved)?.Value;
            var validBoolean = bool.TryParse(value, out bool approved);
            return validBoolean && approved;
        }
    }
}
