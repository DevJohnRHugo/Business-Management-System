using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Domain.Enums;

namespace SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento
{
    public interface IUserCredentialHistory<TCredentialState>
    {
        void AddStates(string userName, string password, IEnumerable<string> roles, int employeeId);

        Task RevertStateAsync<TUserManager, TApplicationUser>(TApplicationUser user, TUserManager userManager, RollbackFrom rollback, string updatePassword);
    }
}
