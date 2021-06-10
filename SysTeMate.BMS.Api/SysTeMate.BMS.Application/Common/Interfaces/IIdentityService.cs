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
        Task<bool> CreateUser(CreateUserCommand request);
        Task<bool> UpdateUser(UpdateUserCommand request);
        Task<bool> DeleteUser(Guid id);
        Task<bool> SignIn(SignInUserCommand request);
        Task SignOut();
        //Task<TApplicationUser> GetApplicationUser<TApplicationUser>(string id);
    }
}
