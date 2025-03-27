using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Exceptions
{

    public class RegistrationFailedException(IEnumerable<string> errorDescriptions)
        : Exception($"Registration failed with following errors: {string.Join(Environment.NewLine, errorDescriptions)}");
}
