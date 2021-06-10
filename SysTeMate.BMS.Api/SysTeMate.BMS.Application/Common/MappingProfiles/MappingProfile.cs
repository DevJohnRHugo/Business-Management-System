using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Application.Common.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserCommand, ApplicationUserVm>()
                .ForMember(dest => dest.Password, act => act.Ignore());

            CreateMap<SignInUserCommand, ApplicationUserVm>()
                .ForMember(dest => dest.Password, act => act.Ignore()); 

            CreateMap<UpdateUserCommand, ApplicationUserVm>()
                .ForMember(dest => dest.Password, act => act.Ignore());

            CreateMap<DeleteUserCommand, ApplicationUserVm>();            
        }
    }
}
