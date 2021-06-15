using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Identity;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento;
using SysTeMate.BMS.Application.ApplicationUsers.Queries;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Helpers;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Domain.Enums;
using SysTeMate.BMS.Infrastructure.ApplicationUsers.Memento;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserManager<ApplicationUser, IdentityResult> _mockUserManager;
        private readonly ISignInManager<SignInResult> _mockSignInManager;
        private readonly IUserCredentialHistory<UserCredentialState> _credentialHistory;

        public IdentityService(IUserManager<ApplicationUser, IdentityResult> mockUserManager, IUserCredentialHistory<UserCredentialState> credentialHistory, ISignInManager<SignInResult> mockSignInManager)
        {
            _mockUserManager = mockUserManager;
            _credentialHistory = credentialHistory;
            _mockSignInManager = mockSignInManager;
        }

        public async Task<bool> CreateUser(CreateUserCommand request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                EmployeeId = request.EmployeeId
            };

            var result = await CreateUserProcess(request, user);

            return result;
        }

        public async Task<bool> SignIn(SignInUserCommand request)
        {
            var result = await _mockSignInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

            return (result.Succeeded) ? true : false;
        }

        public async Task<ApplicationUserListVm> GetApplicationUsers(GetApplicationUserQuery request = null)
        {
            try
            {
                var appUserList = new ApplicationUserListVm();

                if (request.Id != null)
                {
                    var user = await GetApplicationUser(request.Id.Value);

                    if (NullChecker.IsNull(user))
                    {
                        appUserList.ApplicationUsers = null;
                    }
                    else
                    {
                        appUserList.ApplicationUsers = new List<ApplicationUserDto>
                        {
                            new ApplicationUserDto
                            {
                              Id = new Guid(user.Id),
                              UserName = user.UserName,
                              EmployeeId = user.EmployeeId,
                            }
                        };
                    }
                }
                else
                {
                    appUserList.ApplicationUsers = _mockUserManager.Users().ToList().Select(user => new ApplicationUserDto
                    {
                        Id = new Guid(user.Id),
                        UserName = user.UserName,
                        EmployeeId = user.EmployeeId,
                    }).ToList();
                }

                return appUserList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task SignOut()
        {
            await _mockSignInManager.SignOutAsync();
        }

        public async Task<bool> UpdateUser(UpdateUserCommand request)
        {
            var user = await GetApplicationUser(request.Id);
            var currentRoles = await GetApplicationUserRoles(user);

            _credentialHistory.AddStates(user.UserName, request.Password, currentRoles, user.EmployeeId);

            user.UserName = request.UserName;
            user.EmployeeId = request.EmployeeId;

            if (NullChecker.IsNull(user) || !await UpdateUserCredentials(user, request.Password, request.NewPassword))
                return false;

            return await AddUserRoles(user, request.Password, request.NewPassword, request.Roles);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetApplicationUser(id);
            return NullChecker.IsNull(user) ? false : _mockUserManager.DeleteAsync(user).Result.Succeeded;
        }

        private async Task<ApplicationUser> GetApplicationUser(Guid id)
        {
            return await _mockUserManager.FindByIdAsync(id.ToString()) ?? null;
        }

        private async Task<IEnumerable<string>> GetApplicationUserRoles(ApplicationUser user)
        {
            return await _mockUserManager.GetRolesAsync(user) ?? null;
        }

        private async Task<bool> CreateUserProcess(CreateUserCommand request, ApplicationUser user)
        {
            var result = false;
            var createUser = await _mockUserManager.CreateAsync(user, request.Password);
            var addRoles = await _mockUserManager.AddToRolesAsync(user, request.Roles);

            if (!createUser.Succeeded || !addRoles.Succeeded)
            {
                var userId = _mockUserManager.FindByNameAsync(user.UserName).Result.Id;
                await DeleteUser(new Guid(userId));
            }
            else
            {
                result = true;
            }

            return result;
        }

        private async Task<bool> UpdateUserCredentials(ApplicationUser user, string currentPassword, string newPassword)
        {
            var updateUser = await _mockUserManager.UpdateAsync(user);
            var updatePassword = await _mockUserManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!updatePassword.Succeeded)
                await _credentialHistory.RevertState(user, _mockUserManager, RollbackFrom.UserNameAndEmpId, newPassword);

            return !updateUser.Succeeded || !updatePassword.Succeeded ? false : true;
        }

        private async Task<bool> AddUserRoles(ApplicationUser user, string password, string newPassword, IEnumerable<string> roles)
        {
            var result = false;
            await _mockUserManager.RemoveFromRolesAsync(user, await _mockUserManager.GetRolesAsync(user));

            foreach (var role in roles)
            {
                var addRoleResult = await _mockUserManager.AddToRoleAsync(user, role);

                if (!addRoleResult.Succeeded)
                {
                    await _credentialHistory.RevertState(user, _mockUserManager, RollbackFrom.Roles, newPassword);
                    return false;
                }
                else
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
