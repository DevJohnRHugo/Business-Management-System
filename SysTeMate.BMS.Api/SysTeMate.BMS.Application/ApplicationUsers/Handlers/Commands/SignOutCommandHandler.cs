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
    public class SignOutCommandHandler : IRequestHandler<SignOutUserCommand>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public SignOutCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            await _identityService.SignOut();
            return Unit.Value;
        }
    }
}
