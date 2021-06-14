using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Application.ApplicationUsers.Commands
{
    public class SignOutUserCommand : IRequest
    {
        public string UserName { get; set; }
    }
}
