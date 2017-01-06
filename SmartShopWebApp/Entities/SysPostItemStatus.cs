using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class SysPostItemStatus
    {
        public Int32 Id { get; set; }
        public String Status { get; set; }
        public Boolean IsReserved { get; set; }
        public Boolean IsBought { get; set; }
        public Boolean IsAvailable { get; set; }
    }
}