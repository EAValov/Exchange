using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public abstract class Order
    {
        public DateTime CreatedAt { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Commentary { get; set; }
    }

    public class SellOrder : Order
    {
        [Key]
        public int SellOrderID { get; set; }
    }

    public class BuyOrder : Order
    {
        [Key]
        public int BuyOrderID { get; set; }
    }

}