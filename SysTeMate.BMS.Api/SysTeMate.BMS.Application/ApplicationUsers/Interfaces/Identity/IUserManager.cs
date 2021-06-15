using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Identity
{
    public interface IUserManager<TApplicationUser, TIdentityResult>
    {
        IQueryable<TApplicationUser> Users();
        Task<TIdentityResult> DeleteAsync(TApplicationUser applicationUser);
        Task<TApplicationUser> FindByIdAsync(string userId);
        Task<TApplicationUser> FindByNameAsync(string userName);
        Task<IList<string>> GetRolesAsync(TApplicationUser applicationUser);
        Task<TIdentityResult> CreateAsync(TApplicationUser applicationUser, string password);
        Task<TIdentityResult> UpdateAsync(TApplicationUser applicationUser);
        Task<TIdentityResult> AddToRoleAsync(TApplicationUser applicationUser, string role);
        Task<TIdentityResult> AddToRolesAsync(TApplicationUser applicationUser, IEnumerable<string> roles);
        Task<TIdentityResult> ChangePasswordAsync(TApplicationUser applicationUser, string currentPassword, string newPassword);
        Task<TIdentityResult> RemoveFromRolesAsync(TApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
