using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;
using SysTeMate.BMS.Application.ApplicationUsers.Queries;
using SysTeMate.BMS.Application.ApplicationUsers.ViewModels;

namespace SysTeMate.BMS.Application.Common.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateUserCommand, ApplicationUserVm>()
              /*.ForMember(dest => dest.ApplicationUserVms, act => act.MapFrom(a => new ApplicationUserVm { Password = a.Password }))*/;

            CreateMap<CreateUserCommand, ApplicationUserVm>()
                /*.ForMember(dest => dest.ApplicationUserVms.Password, act => act.Ignore())*/;
                       

            CreateMap<SignInUserCommand, ApplicationUserVm>()
                /*.ForMember(dest => dest.ApplicationUserVms.Password, act => act.Ignore())*/;

            CreateMap<SignOutUserCommand, ApplicationUserVm>();

            CreateMap<UpdateUserCommand, ApplicationUserVm>()
                /*.ForMember(dest => dest.ApplicationUserVms.Password, act => act.Ignore())*/;

            CreateMap<DeleteUserCommand, ApplicationUserVm>();

            CreateMap<GetApplicationUserQuery, ApplicationUserVm>();
        }
    }
}
