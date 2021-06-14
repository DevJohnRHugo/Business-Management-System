using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;

namespace SysTeMate.BMS.Application.ApplicationUsers.Validators.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(v => v.AppUserDto.EmployeeId).NotNull();
            RuleFor(v => v.AppUserDto.EmployeeId).NotEmpty();
            RuleFor(v => v.AppUserDto.UserName).NotNull();
            RuleFor(v => v.AppUserDto.UserName).NotEmpty();
            RuleFor(v => v.AppUserDto.Password).NotNull();
            RuleFor(v => v.AppUserDto.Password).NotEmpty();
        }
    }
}
