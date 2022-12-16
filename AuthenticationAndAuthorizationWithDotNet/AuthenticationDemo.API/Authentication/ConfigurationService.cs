using AA.Common.Services;
using AuthenticationDemo.API.Data;
using AuthenticationDemo.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using System;

namespace AuthenticationDemo.API.Authentication
{
    public enum AuthenticationType
    {
        Identity,
        AAD
    }

    public class ConfigurationService
    {
        public static AuthenticationType AuthenticationType { get; private set; }

        public static void ConfigurationAuthenticationService(IConfiguration configuration, IServiceCollection services)
        {
            AuthenticationType = configuration.GetValue<AuthenticationType>("AuthenticationType");

            if(AuthenticationType == AuthenticationType.Identity)
            {
                services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("AppUserDB"));

                services.AddIdentity<User, IdentityRole>(config => {
                    config.Password.RequiredLength = 3;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                })
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddSignInManager<SignInManager<User>>()
                    .AddDefaultTokenProviders();

                services.AddScoped<IUserManager, IdentityUserManager>();

            }
            else if(AuthenticationType == AuthenticationType.AAD)
            {
                // Configure Identity Web Authentication using Azure Active directory
                services.AddMicrosoftIdentityWebApiAuthentication(configuration);
            }
            else
            {
                throw new Exception("Authentication type not configured");
            }
        }
    }
}
