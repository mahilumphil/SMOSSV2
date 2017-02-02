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
        public String BoughtByUser { get; set; }
        public String SenderUserId { get; set; }
        public String RecipientUserId { get; set; }
        public String BoughtDate { get; set; }
        public Boolean IsAccepted { get; set; }
        public String Message { get; set; }
        public Decimal Quantity { get; set; }
        public Decimal PartialAmount { get; set; }
        public String Inquirer { get; set; }
        public String InquirerUserId { get; set; }
        public String InquiredItem { get; set; }
    }
}