using Microsoft.AspNetCore.Authorization;

namespace AuthorizationDemo.API.Authorization.RoleAndPolicy
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
