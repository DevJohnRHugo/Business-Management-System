using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Identity
{
    public interface ISignInManager<TSignInResult>
    {
        Task<TSignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
        Task SignOutAsync();
    }
}
