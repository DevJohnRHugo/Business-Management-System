﻿using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.Common.ViewModels;

namespace SysTeMate.BMS.Application.ApplicationUsers.ViewModels
{
    public class ApplicationUserVm : BaseViewModelReponse
    {
        public ApplicationUserDto AppUserDto { get; set; }
    }
}
