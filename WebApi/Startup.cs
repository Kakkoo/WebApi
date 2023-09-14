using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data;

namespace WebApi
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
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Your API Title",
					Version = "v1",
					Description = "Your API Description"
				});
			});
			services.AddControllers();
			var connectionString = Configuration.GetConnectionString("DefaultConnection");
			// var connectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<NestleDbContext>(options =>
			 options.UseSqlServer(connectionString, sqlServerOptionsAction =>
			 {
				 // Configure other SQL Server options here if needed
				 sqlServerOptionsAction.EnableRetryOnFailure(
					 maxRetryCount: 5, // Maximum number of retries
					 maxRetryDelay: TimeSpan.FromSeconds(30), // Maximum delay between retries
					 errorNumbersToAdd: null // Error numbers to consider for retry (null means all errors)
				 );
			 }));
			// options.UseSqlServer(

			// Configuration.GetConnectionString("DefaultConnection")));
			//services.AddSwaggerGen(c =>
			//{
			//c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
			//});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

			app.UseSwagger();

			// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
			// specifying the Swagger JSON endpoint.
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Title");
			});

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
