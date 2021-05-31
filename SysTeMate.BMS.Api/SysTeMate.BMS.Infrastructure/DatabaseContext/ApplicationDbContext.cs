using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Domain.Entities;
using SysTeMate.BMS.Domain.Enums;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Domain.DatabaseContext
{
    public class ApplicationDbContext : /*ApiAuthorizationDbContext<ApplicationUser>*/ IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        //public ApplicationDbContext(
        //   DbContextOptions options,
        //   IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        //{
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Employee>()
                .Property(e => e.EmployeeTypeId)
                .HasConversion<int>();

            builder
                .Entity<EmployeeType>()
                .Property(e => e.Id)
                .HasConversion<int>();

            builder
                .Entity<EmployeeType>().HasData(
                Enum.GetValues(typeof(EmployeeTypeEnums))
                    .Cast<EmployeeTypeEnums>()
                    .Select(e => new EmployeeType()
                    {
                        Id = e,
                        Name = e.ToString()
                    })
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
