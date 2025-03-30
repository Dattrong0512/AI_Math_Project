using AIMathProject.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Abstracts
{
    public interface IEmailHelper
    {
        Task SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellationToken);
    }
}
