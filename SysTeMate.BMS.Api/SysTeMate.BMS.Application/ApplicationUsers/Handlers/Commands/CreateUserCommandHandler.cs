using AutoMapper;
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

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUserVm>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUserVm = _mapper.Map<ApplicationUserVm>(request);
            var createSuccess = await _identityService.CreateUser(request);

            if (createSuccess)
            {
                applicationUserVm.IsSuccess = true;
                applicationUserVm.Message = "User successfully created!";
            }
            else
            {
                applicationUserVm.Message = "Failed to create user!";
            }

            return applicationUserVm;
        }
    }
}
