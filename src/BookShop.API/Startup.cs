﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository.ServerRepository;
using Repository.Model;

namespace BookShop.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddScoped<BookEntities>(s => new BookEntities(Configuration.GetConnectionString("BookEntities")));
            services.AddSingleton(typeof(IGenericRepository<>), typeof(EFGenericRepository<>));
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<ICatRepository, EFCatRepository>();
            services.AddScoped<ISubCatRepository, EFSubCatRepository>();
            services.AddScoped<IBookRepository, EFBookRepository>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddScoped<IRecommendRepository, EFRecommendRepository>();
            services.AddScoped<ITimeRepository, EFTimeRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}
