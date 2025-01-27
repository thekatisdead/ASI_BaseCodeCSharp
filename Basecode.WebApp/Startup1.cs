﻿using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.Data;

using Hangfire;

using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.Identity.Client;

using Microsoft.Graph.Core;
using Microsoft.Graph;
using Microsoft.Graph.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;


namespace Basecode.WebApp
{
    public partial class Startup1
    {
        public Startup1(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureDependencies(services);       // Configuration for dependency injections           
            this.ConfigureDatabase(services);           // Configuration for database connections
            this.ConfigureMapper(services);             // Configuration for entity model and view model mapping
            this.ConfigureCors(services);               // Configuration for CORS
            this.ConfigureAuth(services);               // Configuration for Authentication logic
            this.ConfigureMVC(services);                // Configuration for MVC                  

            services.AddDistributedMemoryCache();

            // Configure session options
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Required for GDPR compliance
                                                   // You can set other options as needed
            });

            // Add services to the container.
            services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<ApplicationTrackingRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<CurrentHiresRepository>();
            services.AddScoped<JobOpeningRepository>();
            services.AddScoped<BasecodeContext>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddScoped<ITeamsService, TeamsService>();

            // Hangfire for delayed functions
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("AzureConnection")));
            services.AddHangfireServer();

            // Azure AD for generating teams link
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");     // Enables site to redirect to page when an exception occurs
                app.UseHsts();                              // Enables the Strict-Transport-Security header.
            }

            app.UseStaticFiles();           // Enables the use of static files
            app.UseHttpsRedirection();      // Enables redirection of HTTP to HTTPS requests.
            app.UseCors("CorsPolicy");      // Enables CORS                              
            app.UseRouting();
            app.UseAuthentication();        // Enables the ConfigureAuth service.
            app.UseMvc();
            app.UseAuthorization();
            app.UseSession();
            //app.UseHangfireDashboard();     // Hangfire for delayed functions

            this.ConfigureRoutes(app);      // Configuration for API controller routing
            this.ConfigureAuth(app);        // Configuration for Token Authentication
        }

    }
}
