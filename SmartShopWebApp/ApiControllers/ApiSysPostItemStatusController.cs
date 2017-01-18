using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiSysPostItemStatusController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

    


        [HttpGet, Route("api/list/syspostitemstatus")]
        public List<Entities.SysPostItemStatus> listSysPostItemStatus()
        {
            var itemstatus = from d in db.SysPostItemStatus
                             select new Entities.SysPostItemStatus
                             {
                                 Id = d.Id,
                                 Status = d.Status,
                                 IsReserved = d.IsReserved,
                                 IsBought = d.IsBought,
                                 IsAvailable = d.IsAvailable
                             };
            return itemstatus.ToList();
        }

    }
}
