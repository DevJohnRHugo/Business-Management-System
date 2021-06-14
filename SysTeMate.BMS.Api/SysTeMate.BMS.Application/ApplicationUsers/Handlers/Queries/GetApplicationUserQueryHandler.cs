using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SysTeMate.BMS.Application.ApplicationUsers.Queries;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Application.Common.Interfaces;

namespace SysTeMate.BMS.Application.ApplicationUsers.Handlers.Queries
{
    public class GetApplicationUserQueryHandler : IRequestHandler<GetApplicationUserQuery, ApplicationUserListVm>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        public GetApplicationUserQueryHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<ApplicationUserListVm> Handle(GetApplicationUserQuery request, CancellationToken cancellationToken)
        {
            var appUserListVm = new ApplicationUserListVm();
            var result = await _identityService.GetApplicationUsers(request);

            if (result == null)
            {               
                appUserListVm.IsSuccess = false;
                appUserListVm.Message = "Failed to retrieve users or user does not exist";
            }

            return result;
        }
    }
}
