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
        public String Discount { get; set; }
        public Decimal Price { get; set; }
        public Decimal? ItemView { get; set; }
        public Decimal? StatusRate1 { get; set; }
        public Decimal? StatusRate2 { get; set; }
        public Decimal? StatusRate3 { get; set; }
        public Decimal? StatusRate4 { get; set; }
        public Decimal? StatusRate5 { get; set; }
        public Decimal? StatusRateOverAll { get; set; }
        public byte[] PhotoValue { get; set; }
    }
}