using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Interfaces;

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUserVm>
    {
        private readonly ApplicationUserVm _applicationUserVm;
        private readonly IIdentityService _identityService;

        public UpdateUserCommandHandler(ApplicationUserVm applicationUserVm, IIdentityService identityService)
        {
            _applicationUserVm = applicationUserVm;
            _identityService = identityService;
        }

        public async Task<ApplicationUserVm> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _applicationUserVm.EmployeeId = request.EmployeeId;
            _applicationUserVm.UserName = request.UserName;

            var result = await _identityService.UpdateUser(request.Id, request.UserName, request.Password, request.NewPassword, request.Roles);

            return _applicationUserVm;
        }
    }
}
