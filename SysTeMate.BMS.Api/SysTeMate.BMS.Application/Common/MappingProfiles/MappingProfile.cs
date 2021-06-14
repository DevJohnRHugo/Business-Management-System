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
              .ForMember(x => x.AppUserDto, opt => opt.MapFrom(model => new ApplicationUserDto
              {
                  Id = null,
                  UserName = model.UserName,
                  EmployeeId = model.EmployeeId
              }));

            CreateMap<SignInUserCommand, ApplicationUserVm>()
            /*.ForMember(dest => dest.ApplicationUserVms.Password, act => act.Ignore())*/
            .ForMember(x => x.AppUserDto, opt => opt.MapFrom(model => new ApplicationUserDto
             {
                 UserName = model.UserName
             }));

            CreateMap<SignOutUserCommand, ApplicationUserVm>();

            CreateMap<UpdateUserCommand, ApplicationUserVm>()
            //.ForMember(dest => dest.AppUserDto.Password, act => act.Ignore());
            .ForMember(x => x.AppUserDto, opt => opt.MapFrom(model => new ApplicationUserDto
            {
                Id = model.Id,
                UserName = model.UserName,
                EmployeeId = model.EmployeeId
            }));

            CreateMap<DeleteUserCommand, ApplicationUserVm>()
            .ForMember(x => x.AppUserDto, opt => opt.MapFrom(model => new ApplicationUserDto
            {
                Id = model.Id,
                UserName = model.UserName,
                EmployeeId = model.EmployeeId
            }));

            CreateMap<GetApplicationUserQuery, ApplicationUserVm>();
        }
    }
}
