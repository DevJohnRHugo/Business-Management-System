using System;
using System.Collections.Generic;
using System.Text;

namespace SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento
{
    public interface IUserCredentialState
    {
        public object GetCredential();
    }
}
