using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.Payment.TokenPackage
{
    public class TokenPackageDto
    {
        public int TokenPackageId { get; set; }

        public string PackageName { get; set; } = null!;

        public int? Tokens { get; set; }

        public int? Price { get; set; }

        public string? Description { get; set; }

    }
}
