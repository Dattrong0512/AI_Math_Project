using AIMathProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.MethodDto
{
    public class MethodDto
    {
        public int? MethodId { get; set; } 
        public string? MethodName { get; set; }

        public string? MethodIcon { get; set; }
    }

}
