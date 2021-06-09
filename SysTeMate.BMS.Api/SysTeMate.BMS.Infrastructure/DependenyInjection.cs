using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Domain.DatabaseContext;
using SysTeMate.BMS.Infrastructure.DatabaseContext;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Domain
{
    public static class DependencyInjection
    {       
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("BMSContext"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();         

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
