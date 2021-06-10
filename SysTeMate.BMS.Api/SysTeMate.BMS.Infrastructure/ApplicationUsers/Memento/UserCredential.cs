using System;
using System.Collections.Generic;
using System.Text;
using SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento;

namespace SysTeMate.BMS.Infrastructure.ApplicationUsers.Memento
{
    public class UserCredential : IUserCredential<UserCredentialState>
    {
        private object _credential;

        public UserCredentialState createCredentialState()
        {
            return new UserCredentialState(_credential);
        }

        public void Restore(UserCredentialState credentialState)
        {
            _credential = credentialState.GetCredential();
        }

        public void SetContent(object credential)
        {
            _credential = credential;
        }
    }
}
