using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<ApplicationUserVm> CreateAsync(string userName, string password);
    }
}
