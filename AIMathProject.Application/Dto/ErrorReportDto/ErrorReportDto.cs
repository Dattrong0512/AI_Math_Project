using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.ErrorReportDto
{
    public  class ErrorReportDto
    {
        public int ErrorId { get; set; }

        public int? UserId { get; set; }

        public string? ErrorMessage { get; set; }

        public string? ErrorType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public bool? Resolved { get; set; }
    }
}
