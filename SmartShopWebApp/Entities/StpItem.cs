using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class StpItem
    {
        public Int32 Id { get; set; }
        public Byte[] Photo { get; set; }
        public String ItemName { get; set; }
        public Decimal Price { get; set; }
        public Int32 ItemCategoryId { get; set; }
        public String ItemCategory { get; set; }
        public String Specification { get; set; }
        public String UserId { get; set; }
        public String CreatedDate { get; set; }
        public String UpdatedDate { get; set; }
    }
}