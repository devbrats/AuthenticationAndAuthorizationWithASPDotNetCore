using AuthenticationDemo.API.Authentication;
using AuthenticationDemo.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AuthenticationDemo.API
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

            ConfigurationService.ConfigurationAuthenticationService(Configuration, services);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication Demo API", Version = "v1" });
            });

            services.AddScoped<IRepository, AppRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            if(ConfigurationService.AuthenticationType==AuthenticationType.AAD)
            {
                // Configure middleware to authenticate user before accessing the request.
                app.Use(async (context, next) =>
                {
                    if (!context.User.Identity?.IsAuthenticated ?? false)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Invalid User !");
                    }
                    else
                    {
                        await next();
                    }
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
