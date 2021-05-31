using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.DatabaseContext
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserVm _applicationUserVm;

        public IdentityService(UserManager<ApplicationUser> userManager, ApplicationUserVm applicationUserVm)
        {
            _userManager = userManager;
            _applicationUserVm = applicationUserVm;
        }

        public async Task<ApplicationUserVm> CreateAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName
            };

            var result = _userManager.CreateAsync(user, password);

            _applicationUserVm.UserName = userName;
            _applicationUserVm.Password = password;
            _applicationUserVm.IsSuccess = result.Result.Succeeded;

            return _applicationUserVm;
        }
    }
}
