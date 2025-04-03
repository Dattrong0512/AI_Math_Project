using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Exceptions
{
    public class EmailNotConfirmException : Exception  // Renamed to follow exception naming convention
    {
        public EmailNotConfirmException(string email)
            : base($"Email {email} not confirmed, please confirm it")
        {
        }
    }

}
