using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<decimal> Total_Price { get; set; }
        public DateTime? Created_At { get; set; }
    }
}