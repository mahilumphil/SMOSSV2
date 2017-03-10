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
        public String SenderUser { get; set; }
        public String SenderMessageBody { get; set; }
        public String SenderMessageDate { get; set; }
        public String RecipientUserId { get; set; }
        public String RecipientUser { get; set; }
        public String RecipientMessageBody { get; set; }
        public String RecipientMessageDate { get; set; }
        public String MessageBody { get; set; }
    }
}