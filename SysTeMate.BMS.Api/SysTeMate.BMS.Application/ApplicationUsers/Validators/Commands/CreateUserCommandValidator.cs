using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;

namespace SysTeMate.BMS.Application.ApplicationUsers.Validators.Commands
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(v => v.EmployeeId).NotNull();
            RuleFor(v => v.EmployeeId).NotEmpty();
            RuleFor(v => v.UserName).NotNull();
            RuleFor(v => v.UserName).NotEmpty();
            RuleFor(v => v.Password).NotNull();
            RuleFor(v => v.Password).NotEmpty();
        }
    }
}
