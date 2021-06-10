using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;
using SysTeMate.BMS.Infrastructure.Models;

namespace SysTeMate.BMS.Infrastructure.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CreateUserCommand, ApplicationUserVm>();
            //CreateMap<DeleteUserCommand, ApplicationUser>();            
        }
    }
}
