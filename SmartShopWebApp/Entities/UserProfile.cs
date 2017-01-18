using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShopWebApp.Entities
{
    public class UserProfile
    {
        public String Email { get; set; }
        public String FullName { get; set; }
        public String ContactNumber { get; set; }
        public String Address { get; set; }
    }
}