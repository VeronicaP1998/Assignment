using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Comments
    {
        public int ID { get; set; }
        public Nullable<int> Post_ID { get; set; }
        public int Author_ID { get; set; }
        public string Content { get; set; }
        public DateTime? Created_At { get; set; }
    }
}