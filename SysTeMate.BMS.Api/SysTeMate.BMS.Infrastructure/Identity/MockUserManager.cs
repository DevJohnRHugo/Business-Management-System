using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Identity;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.Identity
{
    public class MockUserManager : IUserManager<ApplicationUser, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MockUserManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser applicationUser, string role)
        {
            return await _userManager.AddToRoleAsync(applicationUser, role);
        }

        public async Task<IdentityResult> AddToRolesAsync(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(applicationUser, roles);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser applicationUser, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser applicationUser, string password)
        {
            return await _userManager.CreateAsync(applicationUser, password);
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser applicationUser)
        {
            return await _userManager.DeleteAsync(applicationUser);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId) ?? null;
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser applicationUser)
        {
            return await _userManager.GetRolesAsync(applicationUser);
        }

        public async Task<IdentityResult> RemoveFromRolesAsync(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            return await _userManager.RemoveFromRolesAsync(applicationUser, roles);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser applicationUser)
        {
            return await _userManager.UpdateAsync(applicationUser);
        }

        public IQueryable<ApplicationUser> Users()
        {
            return _userManager.Users;
        }
    }
}
