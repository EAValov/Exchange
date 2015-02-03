using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class CompletedOrder
    {
        public int CompletedOrderID { get; set; }
        public DateTime CompletedAt { get; set; }
        public DateTime SellOrderDate { get; set; }
        public DateTime BuyOrderDate { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string SellComment { get; set; }
        public string BuyComment { get; set; }
    }
}