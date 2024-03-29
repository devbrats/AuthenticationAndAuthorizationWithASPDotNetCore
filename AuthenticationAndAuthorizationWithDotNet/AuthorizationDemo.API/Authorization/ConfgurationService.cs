﻿using AuthorizationDemo.API.Authorization.JWT;
using AuthorizationDemo.API.Authorization.RoleAndPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuthorizationDemo.API.Authorization
{
    public class ConfigurationService
    {
        public static AuthorizationType AuthorizationType { get; private set; }

        public static void ConfigureAuthenticationAndAuthorizationServices(AuthorizationType type, IServiceCollection services)
        {
            AuthorizationType = type ;

            if (type == AuthorizationType.JWT)
            {
                ConfigureAuthenticationAndAuthorizationForJWT(services);
            }
            else
            {
                ConfigureAuthenticationAndAuthorizationForRoleAndPolicy(services);
            }
        }

        private static void ConfigureAuthenticationAndAuthorizationForJWT(IServiceCollection services)
        {
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    // configuring to validate token as a part of query in browser : ${url}?access_token={token}
                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }

                            return Task.CompletedTask;
                        }
                    };

                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = TokenMetadata.Issuer,
                        ValidAudience = TokenMetadata.Audience,
                        IssuerSigningKey = TokenMetadata.SymmetricKey
                    };
                });
        }

        private static void ConfigureAuthenticationAndAuthorizationForRoleAndPolicy(IServiceCollection services)
        {
            services.AddAuthentication("CookieAuth")
                       .AddCookie("CookieAuth", config =>
                       {
                           config.Cookie.Name = "AuthorizationDemoAPICookie";
                           config.LoginPath = "/user/authenticate";
                       });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Claim.Email", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.Email);
                });
                config.AddPolicy("Admin", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.Role);
                });
            });

            services.AddSingleton<IAuthorizationHandler, CustomRequirementClaimHandler>();
        }

    }
}
