using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestWithAsp.NetUdemy.Model.Context;
using RestWithAsp.NetUdemy.Business;
using RestWithAsp.NetUdemy.Business.Implementations;

namespace RestWithAsp.NetUdemy
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
            var connection = Configuration["MySqlConnection:MySqlConnectionString"];

            //Dependency
            services.AddDbContext<MySqlContext>(options => options.UseMySQL(connection));

            services.AddMvc();

            //Dependency API Version
            services.AddApiVersioning();

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //#Dependency Injection #Breno
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
