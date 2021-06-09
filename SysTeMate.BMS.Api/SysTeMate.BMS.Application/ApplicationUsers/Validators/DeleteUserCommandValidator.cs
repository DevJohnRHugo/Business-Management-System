using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;

namespace SysTeMate.BMS.Application.ApplicationUsers.Validators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.UserName).NotNull();
            RuleFor(v => v.UserName).NotEmpty();
        }
    }
}
