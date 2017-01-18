using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiSysPayTypeController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();


        [HttpGet, Route("api/list/syspaytype")]
        public List<Entities.SysPayType> listSysPayType()
        {
            var paytype = from d in db.SysPayTypes
                          select new Entities.SysPayType
                          {
                              Id = d.Id,
                              PayType = d.PayType
                          };
            return paytype.ToList();
        }




    }
}
