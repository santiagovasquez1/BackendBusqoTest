using System.Data.Common;
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
using Microsoft.OpenApi.Models;
using MySql.Data.MySqlClient;
using Busqo.Base;
using CotizacionService.Services;

namespace BuscoWebApi
{
   public class Startup
   {
      public Startup(IConfiguration configuration, IWebHostEnvironment env)
      {
         Configuration = configuration;
         if (env.IsDevelopment())
         {
            Environment.SetEnvironmentVariable("CONNECTION_STRING", @"Server=localhost;Database=buscotest;Uid=root;Pwd=Atila_1205;Port:3306;");
         }
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {

         services.AddControllers();
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BuscoWebApi", Version = "v1" });
         });


         services.AddTransient<ServiceBase, CrearCotizacion>();
         services.AddTransient<ServiceBase, ConsultarCotizaciones>();

         DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
         DatabaseOptions.ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BuscoWebApi v1"));
         }

         app.UseHttpsRedirection();

         app.UseRouting();

         app.UseAuthorization();

         app.UseCors(builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
