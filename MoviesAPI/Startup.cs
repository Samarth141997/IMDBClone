using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDBClone_API.DataContract;
using IMDBClone_API.DataProviders;

namespace MoviesAPI
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {                       
            _config = config ?? throw new ArgumentNullException(nameof(config));            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();                                   

            var connectionstring = _config["ConnectionStrings:MoviesDbConnectionString"];
            services.AddDbContext<MovieContext>(o =>
                o.UseSqlServer(connectionstring)
            );

            services.AddScoped<IMovieService, MovieServiceProvider>();
            services.AddScoped<IActorService, ActorServiceProvider>();
            services.AddScoped<IProducerService, ProducerServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
