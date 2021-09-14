using Microsoft.AspNetCore.Authorization;

namespace RoleAndPolicyAuthorization.AuthorizationHandlers
{
    public class CustomRequirementClaim : IAuthorizationRequirement
    {
        public CustomRequirementClaim(string claimType)
        {
            ClaimType = claimType;
        }

        public string ClaimType { get; }
    }
}
