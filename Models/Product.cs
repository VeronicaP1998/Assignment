using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? total_inventory_value { get; set; }
    }
    public class Inventory
    {
        public decimal? total_inventory_value { get; set; }

    }
}