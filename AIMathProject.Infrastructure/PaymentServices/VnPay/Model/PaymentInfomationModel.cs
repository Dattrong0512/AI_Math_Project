﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMathProject.Infrastructure.PaymentServices.VnPay.Model
{
    public class PaymentInfomationModel
    {
        public int OrderID
        {
            get; set;
        }
        public double Amount
        {
            get;
            set;
        }
        public DateTime CreatedDate
        {
            get; set;
        }

    }
}
