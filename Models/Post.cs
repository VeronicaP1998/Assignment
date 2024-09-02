using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Author_ID { get; set; }
        public DateTime Created_At { get; set; }
    }
    public class PostDTO
    {
        public int ID { get; set; }
        public List<Comments> Comments { get; set; } = new List<Comments>();
    }
}