using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWithAsp.NetUdemy.Model.Context;
using RestWithAsp.NetUdemy.Business;
using RestWithAsp.NetUdemy.Business.Implementattions;
using RestWithAsp.NetUdemy.Repository.Implementattions;
using RestWithAsp.NetUdemy.Repository;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using System;
using RestWithAsp.NetUdemy.Repository.Generic;
using Microsoft.Net.Http.Headers;
using Tapioca.HATEOAS;
using RestWithAsp.NetUdemy.Hypermedia;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using RestWithAsp.NetUdemy.Security.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace RestWithAsp.NetUdemy
{
    public class Startup
    {

        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];

            //Dependency
            services.AddDbContext<MySqlContext>(options => options.UseMySQL(connectionString));

            //Login Dependency
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();

            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfigurations")
            )
            .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                // Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                // Tolerance time for the expiration of a token (used in case
                // of time synchronization problems between different
                // computers involved in the communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            // Enables the use of the token as a means of
            // authorizing access to this project's resources
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });


            //Migration
            ExecuteMigration(connectionString);

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("text/xml"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", MediaTypeHeaderValue.Parse("application/json"));
            }).AddXmlSerializerFormatters();

            //Dependency API Version
            services.AddApiVersioning(option => option.ReportApiVersions = true);

            //Dependency Injection #Breno
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();

            //Generic Dependency
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            //HATOAES Dependency
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "RESTful API With ASP.NET Core 2.0", Version = "v1" });
            });


        }

        private void ExecuteMigration(string connectionString)
        {
            if (_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve(evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();

                }
                catch (Exception ex)
                {
                    _logger.LogCritical("Database migration failed.", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
           });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseMvc(routes =>
            {
                routes.MapRoute
                    (
                        name: "DefaultApi",
                        template: "{controller=values}/{id?}"

                    );
            });
        }
    }
}
