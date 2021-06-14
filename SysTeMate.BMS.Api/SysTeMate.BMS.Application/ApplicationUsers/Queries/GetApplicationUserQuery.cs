using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Application.ApplicationUsers.Queries
{
    public class GetApplicationUserQuery : IRequest<ApplicationUserListVm>
    {
        public Guid? Id { get; set; }
    }
}
