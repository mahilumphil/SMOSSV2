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

        [HttpPost, Route("api/add/syspostitemstatus")]
        public HttpResponseMessage addSysPostItemStatus(Entities.SysPostItemStatus itemStatus)
        {
            try
            {
                Data.SysPostItemStatus newSysPostItemStatus = new Data.SysPostItemStatus();

                newSysPostItemStatus.Id = itemStatus.Id;
                newSysPostItemStatus.Status = itemStatus.Status;
                newSysPostItemStatus.IsReserved = itemStatus.IsReserved;
                newSysPostItemStatus.IsBought = itemStatus.IsBought;
                newSysPostItemStatus.IsAvailable = itemStatus.IsAvailable;

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }



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


        [HttpDelete, Route("api/delete/syspostitemstatus/{id}")]
        public HttpResponseMessage deleteSysPostItemStatus(String id)
        {
            try
            {
                var itemStatus = from d in db.SysPostItemStatus
                                 where d.Id == Convert.ToInt32(id)
                                 select d;

                if (itemStatus.Any())
                {
                    db.SysPostItemStatus.DeleteOnSubmit(itemStatus.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            
        }


        [HttpPut, Route("api/update/syspostitemstatus/{id}")]
        public HttpResponseMessage updateSysPostItemStatus(String id, Entities.SysPostItemStatus syspostitemstatus)
        {
            try
            {
                var actSysPostItemStatus = from d in db.SysPostItemStatus
                                           where d.Id == Convert.ToInt32(id)
                                           select d;

                if (actSysPostItemStatus.Any())
                {
                    var updateSysPostItemStatus = actSysPostItemStatus.FirstOrDefault();
                    updateSysPostItemStatus.Id = syspostitemstatus.Id;
                    updateSysPostItemStatus.Status = syspostitemstatus.Status;
                    updateSysPostItemStatus.IsReserved = syspostitemstatus.IsReserved;
                    updateSysPostItemStatus.IsBought = syspostitemstatus.IsBought;
                    updateSysPostItemStatus.IsAvailable = syspostitemstatus.IsAvailable;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
