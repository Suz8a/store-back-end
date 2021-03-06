using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TroquelApi.Models;
using TroquelApi.Services;

namespace TroquelApi
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
            // requires using Microsoft.Extensions.Options
            services.Configure<TroquelDatabaseSettings>(
                Configuration.GetSection(nameof(TroquelDatabaseSettings)));

            services.AddSingleton<ITroquelDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<TroquelDatabaseSettings>>().Value);

            services.AddSingleton<ClienteService>();
            services.AddSingleton<JoyaService>();
            services.AddSingleton<RolService>();
            services.AddSingleton<ServicioService>();
            services.AddSingleton<EstadoService>();
            services.AddSingleton<MaterialService>();

            services.AddControllers();
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
