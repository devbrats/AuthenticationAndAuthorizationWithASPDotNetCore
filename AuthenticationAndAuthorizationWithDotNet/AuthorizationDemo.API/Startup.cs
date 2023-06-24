using AA.Common.Services;
using AuthorizationDemo.API.Authorization;
using AuthorizationDemo.API.Authorization.JWT;
using Microsoft.OpenApi.Models;

namespace AuthorizationDemo.API
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
            var authorizationScheme = Configuration.GetValue<AuthorizationType>("AuthorizationType");
            if(authorizationScheme == AuthorizationType.JWT)
            {
                TokenMetadata.Init(Configuration);
            }

            ConfigurationService.ConfigureAuthenticationAndAuthorizationServices(authorizationScheme, services);

            services.AddControllers();

            services.AddEndpointsApiExplorer();

            // Configure Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Authorization Demo API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                 Type=ReferenceType.SecurityScheme,
                                 Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            services.AddScoped<IUserManager, CommonUserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

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
