using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
//using SysTeMate.BMS.Application.Common.Abstracts;
using SysTeMate.BMS.Application.Common.Interfaces;

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUserVm>
    {
        private readonly ApplicationUserVm _applicationUserVm;        
        private readonly IIdentityService _identityService;

        public CreateUserCommandHandler(ApplicationUserVm applicationUserVm, IIdentityService identityService)
        {
            _applicationUserVm = applicationUserVm;
            _identityService = identityService;
        }

        public async Task<ApplicationUserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _applicationUserVm.EmployeeId = request.EmployeeId;
            _applicationUserVm.UserName = request.UserName;

            var createSuccess = _identityService.CreateUser(request.UserName, request.Password, request.Roles).Result;

            if (createSuccess)
            {               
                _applicationUserVm.IsSuccess = true;
                _applicationUserVm.Message = "User successfully created!";
            }

            return _applicationUserVm;
        }
    }
}
