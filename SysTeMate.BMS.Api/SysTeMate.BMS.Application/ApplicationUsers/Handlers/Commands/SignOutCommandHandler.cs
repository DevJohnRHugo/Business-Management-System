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

        public SignOutCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Unit> Handle(SignOutUserCommand request, CancellationToken cancellationToken)
        {
            await _identityService.SignOut();
            return Unit.Value;
        }
    }
}
