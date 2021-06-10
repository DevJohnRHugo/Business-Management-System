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

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers
{
    public class SignOutCommandHandler : IRequestHandler<SignOutUserCommand, ApplicationUserVm>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public SignOutCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUserVm = _mapper.Map<ApplicationUserVm>(request);
            await _identityService.SignOut();

            applicationUserVm.Message = "Successfully signed out";         

            return applicationUserVm;
        }
    }
}
