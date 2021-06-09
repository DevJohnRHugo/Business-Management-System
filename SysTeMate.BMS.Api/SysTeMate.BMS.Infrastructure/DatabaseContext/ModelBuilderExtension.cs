using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SysTeMate.BMS.Domain.Entities;
using SysTeMate.BMS.Domain.Enums;

namespace SysTeMate.BMS.Infrastructure.DatabaseContext
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder builder)
        {          

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
                Enum.GetValues(typeof(EmployeeTypesEnum))
                    .Cast<EmployeeTypesEnum>()
                    .Select(e => new EmployeeType()
                    {
                        Id = e,
                        Name = e.ToString(),
                    })
            );

            builder
                .Entity<IdentityRole>().HasData(new List<IdentityRole>
                {
                    new IdentityRole
                    {
                         Id = Guid.NewGuid().ToString(),
                         Name = "CanManagerOrders",
                         NormalizedName = "CanManagerOrders".ToUpper(),
                    },
                    new IdentityRole
                    {
                         Id = Guid.NewGuid().ToString(),
                         Name = "CanProcessOrders",
                         NormalizedName = "CanProcessOrders".ToUpper(),
                    },
                    new IdentityRole
                    {
                         Id = Guid.NewGuid().ToString(),
                         Name = "CanAddToInventory",
                         NormalizedName = "CanAddToInventory".ToUpper(),
                    },
                    new IdentityRole
                    {
                         Id = Guid.NewGuid().ToString(),
                         Name = "CanRemoveAnInventory",
                         NormalizedName = "CanRemoveAnInventory".ToUpper(),
                    },
                    new IdentityRole
                    {
                         Id = Guid.NewGuid().ToString(),
                         Name = "CanManageUserAccounts",
                         NormalizedName = "CanManageUserAccounts".ToUpper(),
                    },
                });
        }
    }
}
