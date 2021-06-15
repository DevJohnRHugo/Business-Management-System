using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Interfaces;

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers.Commands
{
    public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, ApplicationUserVm>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public SignInUserCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {           
            var applicationUserVm = _mapper.Map<ApplicationUserVm>(request);
            var result = await _identityService.SignIn(request);

            if (result)
            {
                applicationUserVm.IsSuccess = true;
                applicationUserVm.Message = "Successfully signed in";
            }
            else
            {
                applicationUserVm.Message = "Failed to sign in!";
            }

            return applicationUserVm;
        }
    }
}
