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
            services.AddScoped<IPersonRepository, PersonRepositoryImplementattion>();

            //Generic Dependency
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            //HATOAES Dependency
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            services.AddSingleton(filterOptions);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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
