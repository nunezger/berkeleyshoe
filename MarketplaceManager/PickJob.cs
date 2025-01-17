﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceManager
{
    public class PickJob
    {
        public string Code { get; set; }

        public List<OrderItemAudit> Items { get; set; }

        public string testing { get; set; }
    }

    public class OrderItemAudit
    {
        public string OrderID { get; set; }

        public string Sku { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }
    }
}
