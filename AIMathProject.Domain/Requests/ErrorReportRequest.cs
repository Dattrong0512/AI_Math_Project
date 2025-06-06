using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Domain.Requests
{
    public record ErrorReportRequest
    {
        public required string ErrorMessage { get; init; }
    }
}
