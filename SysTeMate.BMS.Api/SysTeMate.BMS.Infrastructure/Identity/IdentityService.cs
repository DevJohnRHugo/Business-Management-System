using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento;
using SysTeMate.BMS.Application.ApplicationUsers.Queries;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Helpers;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Domain.Constants;
using SysTeMate.BMS.Domain.Entities;
using SysTeMate.BMS.Domain.Enums;
using SysTeMate.BMS.Infrastructure.ApplicationUsers.Memento;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserCredentialHistory<UserCredentialState> _credentialHistory;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<ApplicationUser> userManager/*, RoleManager<IdentityRole> roleManager*/, IUserCredentialHistory<UserCredentialState> credentialHistory, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            //_roleManager = roleManager;
            _credentialHistory = credentialHistory;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateUser(CreateUserCommand request)
        {
            var user = new ApplicationUser
            {
                UserName = request.AppUserDto.UserName,
                EmployeeId = request.AppUserDto.EmployeeId
            };

            var result = await CreateUserProcess(request, user);

            return result;
        }

        public async Task<bool> SignIn(SignInUserCommand request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.AppUserDto.UserName, request.AppUserDto.Password, false, false);

            return (result.Succeeded) ? true : false;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> UpdateUser(UpdateUserCommand request)
        {
            var user = await GetApplicationUser(request.AppUserDto.Id);
            var currentRoles = await GetApplicationUserRoles(user);

            _credentialHistory.AddStates(user.UserName, request.AppUserDto.Password, currentRoles, user.EmployeeId);

            user.UserName = request.AppUserDto.UserName;
            user.EmployeeId = request.AppUserDto.EmployeeId;

            if (NullChecker.IsNull(user) || !await UpdateUserCredentials(user, request.AppUserDto.Password, request.AppUserDto.NewPassword))
                return false;

            return await AddUserRoles(user, request.AppUserDto.Password, request.AppUserDto.NewPassword, request.AppUserDto.Roles);
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await GetApplicationUser(id);
            return NullChecker.IsNull(user) ? false : _userManager.DeleteAsync(user).Result.Succeeded;
        }

        public async Task<ApplicationUserListVm> GetApplicationUsers(GetApplicationUserQuery request = null)
        {
            try
            {
                var appUserList = new ApplicationUserListVm();

                if (request.Id != null)
                {
                    var user = await GetApplicationUser(request.Id.Value);
                    appUserList.ApplicationUserVms = new List<ApplicationUserDto>
                    {
                        new ApplicationUserDto
                        {
                          UserName = user.UserName,
                          Password = user.PasswordHash,
                          EmployeeId = user.EmployeeId,
                          Roles = _userManager.GetRolesAsync(user).Result
                        }
                    };
                }
                else
                {
                    appUserList.ApplicationUserVms = _userManager.Users.ToList().Select(x => new ApplicationUserDto
                    {
                        UserName = x.UserName,
                        Password = x.PasswordHash,
                        EmployeeId = x.EmployeeId,
                        Roles = _userManager.GetRolesAsync(x).Result
                    }).ToList();
                }

                return appUserList;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private async Task<ApplicationUser> GetApplicationUser(Guid id)
        {
            return await _userManager.FindByIdAsync(id.ToString()) ?? null;
        }

        private async Task<IEnumerable<string>> GetApplicationUserRoles(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user) ?? null;
        }

        private async Task<bool> CreateUserProcess(CreateUserCommand request, ApplicationUser user)
        {
            var result = false;
            var createUser = await _userManager.CreateAsync(user, request.AppUserDto.Password);
            var addRoles = await _userManager.AddToRolesAsync(user, request.AppUserDto.Roles);

            if (!createUser.Succeeded && !addRoles.Succeeded)
            {
                var userId = _userManager.FindByNameAsync(user.UserName).Result.Id;
                await DeleteUser(new Guid(userId));
            }
            else
            {
                result = true;
            }

            return result;
        }

        private async Task<bool> UpdateUserCredentials(ApplicationUser user, string password, string newPassword)
        {
            var updateUser = await _userManager.UpdateAsync(user);
            var updatePassword = await _userManager.ChangePasswordAsync(user, password, newPassword);

            if (!updatePassword.Succeeded)
                await _credentialHistory.RevertStateAsync(user, _userManager, RollbackFrom.UserNameAndEmpId, newPassword);

            return !updateUser.Succeeded || !updatePassword.Succeeded ? false : true;
        }

        private async Task<bool> AddUserRoles(ApplicationUser user, string password, string newPassword, IEnumerable<string> roles)
        {
            var result = false;
            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));

            foreach (var role in roles)
            {
                var addRoleResult = await _userManager.AddToRoleAsync(user, role);

                if (!addRoleResult.Succeeded)
                {
                    await _credentialHistory.RevertStateAsync(user, _userManager, RollbackFrom.Roles, newPassword);
                    return false;
                }
                else
                    result = addRoleResult.Succeeded;
            }

            return result;
        }
    }
}
