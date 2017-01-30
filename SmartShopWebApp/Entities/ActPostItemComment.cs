using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class ActPostItemComment
    {
        public Int32 Id { get; set; }
        public Int32 PostId { get; set; }
        public String Post { get; set; }
        public String Comment { get; set; }
        public String CommentByUserId { get; set; }
        public String CommentByUser { get; set; }
        public String CommentDate { get; set; }
        public String UpdatedDate { get; set; }
    }
}