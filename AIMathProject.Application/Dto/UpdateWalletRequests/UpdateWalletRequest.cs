using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Application.Dto.UpdateWalletRequests
{
    public class UpdateWalletRequest
    {
        public int WalletId { get; set; }
        public int Amount { get; set; }
    }
}

