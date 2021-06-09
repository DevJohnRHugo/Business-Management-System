using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Interfaces;

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApplicationUserVm>
    {
        private readonly ApplicationUserVm _applicationUserVm;
        private readonly IIdentityService _identityService;

        public DeleteUserCommandHandler(IIdentityService identityService, ApplicationUserVm applicationUserVm)
        {
            _identityService = identityService;
            _applicationUserVm = applicationUserVm;
        }

        public async Task<ApplicationUserVm> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _applicationUserVm.EmployeeId = request.EmployeeId;
            _applicationUserVm.UserName = request.UserName;
      
            var deleteSuccess =  await _identityService.DeleteUser(request.Id);

            if (deleteSuccess)
            {
                _applicationUserVm.IsSuccess = true;
                _applicationUserVm.Message = "user successfuly deleted";
            }

            return _applicationUserVm;
        }
    }
}
