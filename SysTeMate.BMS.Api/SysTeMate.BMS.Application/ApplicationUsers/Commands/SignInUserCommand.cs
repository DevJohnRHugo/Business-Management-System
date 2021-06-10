using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Application.ApplicationUsers.Commands
{
    public class SignInUserCommand : IRequest<ApplicationUserVm>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
