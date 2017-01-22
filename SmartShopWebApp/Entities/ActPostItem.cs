using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class ActPostItem
    {
        public Int32 Id { get; set; }
        public Int32 ItemId { get; set; }
        public String ItemName { get; set; }
        public String Specification { get; set; }
        public String Remarks { get; set; }
        public String PostDate { get; set; }
        public String ExpiredDate { get; set; }
        public String UpdatedDate { get; set; }
        public Decimal Quantity { get; set; }
        public String PostedByUserId { get; set; }
        public String PostedByUser { get; set; }
        public Boolean IsApproved { get; set; }
        public Int32 PayTypeId { get; set; }
        public String PayType { get; set; }
        public Int32 StatusId { get; set; }
        public String Status { get; set; }
        public Decimal Price { get; set; }
        public byte[] PhotoValue { get; set; }
    }
}