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
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUserVm>
    {        
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ApplicationUserVm> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var applicationUserVm = _mapper.Map<ApplicationUserVm>(request);
            var result = await _identityService.UpdateUser(request);

            if (result)
            {
                applicationUserVm.IsSuccess = true;
                applicationUserVm.Message = "user successfuly updated";
            }
            else
            {
                applicationUserVm.Message = "Failed to update user!";
            }

            return applicationUserVm;
        }
    }
}
