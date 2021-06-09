using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Domain.Enums;

namespace SysTeMate.BMS.Domain.Entities
{
    public class EmployeeType
    {
        public EmployeeTypesEnum Id { get; set; }

        public string Name { get; set; }

        //public List<Employee> Emplyees { get; set; }

    }
}
