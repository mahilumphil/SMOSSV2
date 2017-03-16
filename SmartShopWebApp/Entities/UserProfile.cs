using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class UserProfile
    {
        public Int32 Id { get; set; }
        public String Site { get; set; }
        public Int32 PostId { get; set; }
        public String Email { get; set; }
        public String FullName { get; set; }
        public String ContactNumber { get; set; }
        public String Address { get; set; }
        public String Type { get; set; }
        public Byte[] ProfilePicture { get; set; }
    }
}