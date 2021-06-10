using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento;
using SysTeMate.BMS.Application.Common.Interfaces;
using SysTeMate.BMS.Domain.Enums;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.ApplicationUsers.Memento
{
    public class UserCredentialHistory : IUserCredentialHistory<UserCredentialState>
    {
        private IDictionary<string, UserCredentialState> _credentialStates = new Dictionary<string, UserCredentialState>();
        private readonly IUserCredential<UserCredentialState> _userCredential;

        public UserCredentialHistory(IUserCredential<UserCredentialState> userCredential)
        {
            _userCredential = userCredential;
        }


        public void AddStates(string userName, string password, IEnumerable<string> roles, int employeeId)
        {
            AddAllCredentialState(userName, password, roles, employeeId);
        }

        public async Task RevertStateAsync<TUserManager, TApplicationUser>(TApplicationUser user, TUserManager userManager, RollbackFrom rollback, string updatePassword)
        {
            await RevertStateProcess(user as ApplicationUser, updatePassword, userManager as UserManager<ApplicationUser>, rollback);
        }

        private async Task RevertStateProcess(ApplicationUser user, string updatedPassword, UserManager<ApplicationUser> userManager, RollbackFrom rollbackFrom)
        {
            user.UserName = GetValue("userName").GetCredential().ToString();
            user.EmployeeId = (int)(GetValue("employeeId").GetCredential());
            var prevPassword = GetValue("password").GetCredential().ToString();
            var roles = GetValue("roles").GetCredential() as IEnumerable<string>;

            switch (rollbackFrom)
            {
                case RollbackFrom.UserNameAndEmpId:
                    await userManager.UpdateAsync(user);
                    break;
                case RollbackFrom.Password:
                    await RevertFromPassword(user, updatedPassword, userManager, prevPassword);
                    break;
                default:
                    await RevertRolesAsync(user, roles, userManager);
                    break;
            }
        }

        private async Task RevertFromPassword(ApplicationUser user, string updatedPassword, UserManager<ApplicationUser> userManager, string prevPassword)
        {
            await userManager.UpdateAsync(user);
            await userManager.ChangePasswordAsync(user, updatedPassword, prevPassword);
        }

        private async Task RevertFromRolesAsync(ApplicationUser user, string updatedPassword, UserManager<ApplicationUser> userManager, string prevPassword, IEnumerable<string> roles)
        {
            await RevertFromPassword(user, updatedPassword, userManager, prevPassword);
            await RevertRolesAsync(user, roles, userManager);
        }

        private async Task RevertRolesAsync(ApplicationUser user, IEnumerable<string> roles, UserManager<ApplicationUser> userManager)
        {
            await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));

            foreach (var role in roles)
                await userManager.AddToRoleAsync(user, role);
        }

        private UserCredentialState GetValue(string key)
        {
            UserCredentialState credentialState;
            _credentialStates.TryGetValue(key, out credentialState);

            return credentialState;
        }

        private void AddAllCredentialState(string userName, string password, IEnumerable<string> roles, int employeeId)
        {
            _userCredential.SetContent(userName);
            _credentialStates.Add("userName", _userCredential.createCredentialState());

            _userCredential.SetContent(password);
            _credentialStates.Add("password", _userCredential.createCredentialState());

            _userCredential.SetContent(employeeId);
            _credentialStates.Add("employeeId", _userCredential.createCredentialState());

            _userCredential.SetContent(roles);
            _credentialStates.Add("roles", _userCredential.createCredentialState());
        }
    }
}
