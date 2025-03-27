﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Exceptions
{
    public class ExternalLoginProviderException(string provider, string 
        message):Exception($"External login provider: {provider} error occured:{message}");

}
