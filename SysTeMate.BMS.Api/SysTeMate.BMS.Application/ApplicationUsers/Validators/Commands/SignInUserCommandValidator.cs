﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Commands;

namespace SysTeMate.BMS.Application.ApplicationUsers.Validators.Commands
{
    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommand>
    {
        public SignInUserCommandValidator()
        {
            RuleFor(v => v.UserName).NotNull();
            RuleFor(v => v.UserName).NotEmpty();
            RuleFor(v => v.Password).NotNull();
            RuleFor(v => v.Password).NotEmpty();
        }
    }
}
