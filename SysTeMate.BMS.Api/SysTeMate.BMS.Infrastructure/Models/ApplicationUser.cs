using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Domain.Entities;

namespace SysTeMate.BMS.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        private Employee Employee { get; set; }

        public int EmployeeId { get; set; }
    }
}
