using System;
using System.Collections.Generic;
using System.Text;

namespace SysTeMate.BMS.Application.ApplicationUsers.Interfaces.Memento
{
    public interface IUserCredential<T>
    {
        public T createCredentialState();

        public void Restore(T credentialState);

        public void SetContent(object credential);
    }
}
