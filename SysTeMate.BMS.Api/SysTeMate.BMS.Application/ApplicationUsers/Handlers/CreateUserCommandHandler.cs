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
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUserVm>
    {       
        private readonly ApplicationUserVm _applicationUserVm;
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        //private readonly ApplicationUser

        public CreateUserCommandHandler(IApplicationDbContext context, ApplicationUserVm applicationUserVm, IIdentityService identityService)
        {
            _context = context;
            _applicationUserVm = applicationUserVm;
            _identityService = identityService;
        }

        public async Task<ApplicationUserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _applicationUserVm.EmployeeId = request.EmployeeId;
            _applicationUserVm.UserName = request.UserName;
            _applicationUserVm.IsSuccess = true;
            _applicationUserVm.Message = "Ok";

            await _identityService.CreateAsync(_applicationUserVm.UserName, _applicationUserVm.Password);

            return _applicationUserVm;
        }
    }
}
