using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Api.Models;
using Api.Services.AddressValidation;
using Api.Services.Authentication;
using Api.Services.Config;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
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
            services.AddControllers().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Address Validation",
                    Version = "v1",
                    Description = "An API to perform addresses validation",
                    Contact = new OpenApiContact
                    {
                        Name = "Chris Calviello",
                        Email = "chris.calviello@gmail.com",
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var addressesFormatPath = ConfigurationManager.AppSettings["AddressesFormatPath"].ToString();
            services.AddSingleton<IConfigService>(new JsonConfigService(addressesFormatPath));
            services.AddSingleton<IAddressValidationService, AddressValidationService>();

            var usersPath = ConfigurationManager.AppSettings["UsersPath"].ToString();
            var jwtSecret = ConfigurationManager.AppSettings["JwtSecret"].ToString();
            var jwtDurationInSeconds = double.Parse(ConfigurationManager.AppSettings["JwtDurationInSeconds"].ToString());
            services.AddSingleton<IAuthenticationService>(new LocalAuthenticationService(usersPath, jwtSecret, jwtDurationInSeconds));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
