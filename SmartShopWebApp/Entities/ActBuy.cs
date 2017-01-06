using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class ActBuy
    {
        public Int32 Id { get; set; }
        public Int32 PostId { get; set; }
        public String Post { get; set; }
        public String BoughtByUserId { get; set; }
        public String BoughtDate { get; set; }
        public Boolean IsAccepted { get; set; }
    }
}