using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class ActMessaging
    {
        public Int32 Id { get; set; }
        public String SenderUserId { get; set; }
        public String RecipientUserId { get; set; }
        public String MessageBody { get; set; }
        public String MessageDate { get; set; }
    }
}