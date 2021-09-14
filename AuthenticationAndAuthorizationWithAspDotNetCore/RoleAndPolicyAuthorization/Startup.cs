using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace RoleAndPolicyAuthorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAuthentication("CookieAuth")
              .AddCookie("CookieAuth", config =>
              {
                  config.Cookie.Name = "TestCookie";
                  config.LoginPath = "/WeatherForecast/index";
              });

            services.AddAuthorization(config =>
            {
                config.AddPolicy("Claim.DOB", policyBuilder =>
                {
                    policyBuilder.AddRequirements(new CustomRequirementClaim(ClaimTypes.DateOfBirth));
                });
            });

            services.AddScoped<IAuthorizationHandler, CustomRequirementClaimHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
