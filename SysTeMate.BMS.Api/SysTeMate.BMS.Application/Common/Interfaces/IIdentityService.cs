using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Domain.Entities;

namespace SysTeMate.BMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {     
        Task<bool> CreateUser(string userName, string password, IEnumerable<string> roles);
        Task<bool> UpdateUser(string id, string userName, string password, string newPassword, IEnumerable<string> roles);
        Task CreateRole(string roleType);
        Task<bool> DeleteUser(string userName);
    }
}
