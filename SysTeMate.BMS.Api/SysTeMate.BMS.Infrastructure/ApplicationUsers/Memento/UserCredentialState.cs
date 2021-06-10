using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento;

namespace SysTeMate.BMS.Infrastructure.ApplicationUsers.Memento
{
    public class UserCredentialState : IUserCredentialState
    {
        private readonly object _credential;

        public UserCredentialState(object credential)
        {
            _credential = credential;
        }

        public object GetCredential()
        {
            return _credential;
        }
    }
}
