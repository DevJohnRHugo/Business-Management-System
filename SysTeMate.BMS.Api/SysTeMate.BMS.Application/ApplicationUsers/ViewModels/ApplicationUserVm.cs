using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.Common.ViewModels;
using SysTeMate.BMS.Domain.Entities;

namespace SysTeMate.BMS.Application.ApplicationUsers.ViewModels
{
    public class ApplicationUserVm : BaseViewModelReponse
    {
        //public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        private Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
