using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Domain.Enum;

namespace SysTeMate.BMS.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int EmployeeTypeId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string EmployeeAddress { get; set; }
        public DateTime Birthdate { get; set; }

    }
}
