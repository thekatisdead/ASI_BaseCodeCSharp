﻿using Microsoft.Extensions.DependencyInjection;

namespace Basecode.WebApp
{
    public partial class Startup1
    {
        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }
    }
}
