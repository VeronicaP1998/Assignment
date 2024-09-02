using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}