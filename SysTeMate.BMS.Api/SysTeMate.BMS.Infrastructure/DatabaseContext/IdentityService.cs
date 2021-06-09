using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Domain.Constants;
using SysTeMate.BMS.Domain.Entities;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.DatabaseContext
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<bool> CreateUser(string id, string password, IEnumerable<string> roles)
        {
            try
            {
                var user = new ApplicationUser { Id = id };
                var createUser = await _userManager.CreateAsync(user, password);

                if (!createUser.Succeeded)
                    return false;

                return _userManager.AddToRolesAsync(user, roles).Result.Succeeded;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> UpdateUser(string id, string userName, string password, string newPassword, IEnumerable<string> roles)
        {
            try
            {
               // var result = false;
                var user = await GetApplicationUser(id);
                user.UserName = userName;

                if (IsNull(user) || !await UpdateUserCredentialsAsync(user, password, newPassword))
                    return false;

                //var updateUser = await _userManager.UpdateAsync(user);
                //var updatePassword = await _userManager.ChangePasswordAsync(user, password, newPassword);

                //if (IsNull(updateUser) || !updateUser.Succeeded || !updatePassword.Succeeded)
                //    return false;

                //if (roles.Count() > 0)
                //{
                //    var removeRoles = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

                //    if (removeRoles.Succeeded)
                //    {
                //        foreach (var role in roles)
                //        {
                //            var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                //            result = (addRoleResult.Succeeded) ? true : false;
                //        }
                //    }
                //}

                return await UpdateUserRolesAsync(user, password, newPassword, roles);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await GetApplicationUser(id);

            return IsNull(user) ? false : _userManager.DeleteAsync(user).Result.Succeeded;
        }

        public async Task CreateRole(string roleType)
        {
            var identityRole = new IdentityRole { Name = roleType };
            var createRole = await _roleManager.CreateAsync(identityRole);
        }

        private async Task<ApplicationUser> GetApplicationUser(string id)
        {
            try
            {
                return await _userManager.FindByIdAsync(id) ?? null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private bool IsNull<T>(T model)
        {
            return (model == null) ? true : false;
        }

        private async Task<bool> UpdateUserCredentialsAsync(ApplicationUser user, string password, string newPassword)
        {
            var updateUser = await _userManager.UpdateAsync(user);
            var updatePassword = await _userManager.ChangePasswordAsync(user, password, newPassword);

            return (!updateUser.Succeeded || !updatePassword.Succeeded) ? false : true;
        }

        private async Task<bool> UpdateUserRolesAsync(ApplicationUser user, string password, string newPassword, IEnumerable<string> roles)
        {
            var result = false;

            if (roles.Count() > 0)
            {
                var removeRoles = await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

                if (removeRoles.Succeeded)
                {
                    foreach (var role in roles)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                        result = (addRoleResult.Succeeded) ? true : false;
                    }
                }
            }

            return result;
        }
    }
}
