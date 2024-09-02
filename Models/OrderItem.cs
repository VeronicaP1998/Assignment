using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Price { get; set; }
    }
}