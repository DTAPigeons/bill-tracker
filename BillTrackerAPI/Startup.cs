﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using BillTrackerAPI.Data.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BillTrackerAPI.Data.MongoDB;
using BillTrackerAPI.Data;

namespace BillTrackerAPI
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
            services.AddRazorPages();

            
            services.Configure<MongoDBSettings>(
                Configuration.GetSection(nameof(MongoDBSettings)));

            services.AddSingleton<IMongoDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddSingleton<UserService>();
            services.AddSingleton<BillService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Biil Tracker", Version = "v1" });
            });

            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());

            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";
                options.RequireHttpsMetadata = false;

                options.Audience = "trackerApi";
            });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:44388")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                /*
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("https://localhost:5001")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
                */
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill Tracker V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseRouting();

            app.UseCors("default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "userName",
                    pattern: "{controller=Users}/{accountName}"
                    );
            });

            app.UseHttpsRedirection();
            
        }
    }
}
