using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Domain.Entities;

namespace SysTeMate.BMS.Application.ApplicationUsers.Commands
{
    public class UpdateUserCommand : IRequest<ApplicationUserVm>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public IEnumerable<string> Roles { get; set; }
        //public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        //public UpdateUserCommand()
        //{
        //    AppUserDto = new ApplicationUserDto();
        //}

        //public ApplicationUserDto AppUserDto { get; set; }
    }
}
