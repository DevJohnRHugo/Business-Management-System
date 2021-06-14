using AutoMapper;
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

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers.Commands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApplicationUserVm>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public DeleteUserCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUserVm = _mapper.Map<ApplicationUserVm>(request);
            var deleteSuccess = await _identityService.DeleteUser(request.AppUserDto.Id);

            if (deleteSuccess)
            {
                applicationUserVm.IsSuccess = true;
                applicationUserVm.Message = "user successfuly deleted";
            }
            else
            {
                applicationUserVm.Message = "Failed to delete user!";
            }

            return applicationUserVm;
        }
    }
}
