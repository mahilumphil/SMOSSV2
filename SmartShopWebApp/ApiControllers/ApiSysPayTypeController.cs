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

        [HttpPost, Route("api/add/syspaytype")]
        public HttpResponseMessage addSysPayType(Entities.SysPayType add)
        {
            try
            {
                Data.SysPayType newSysPayType = new Data.SysPayType();

                newSysPayType.Id = add.Id;
                newSysPayType.PayType = add.PayType;

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.OK);
            }

        }


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



        [HttpDelete, Route("api/delete/syspaytype/{id}")]
        public HttpResponseMessage deleteSysPayType(String id)
        {
            try
            {
                var delSysPayType = from d in db.SysPayTypes
                                    where d.Id == Convert.ToInt32(id)
                                    select d;

                if (delSysPayType.Any())
                {
                    db.SysPayTypes.DeleteOnSubmit(delSysPayType.First());
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


        [HttpPut, Route("api/update/syspaytype/{id}")]
        public HttpResponseMessage updateSysPayType(String id, Entities.SysPayType syspaytype)
        {
            try
            {
                var actSysPayType = from d in db.SysPayTypes
                                    where d.Id == Convert.ToInt32(id)
                                    select d;

                if (actSysPayType.Any())
                {
                    var updateSysPayType = actSysPayType.FirstOrDefault();
                    updateSysPayType.Id = syspaytype.Id;
                    updateSysPayType.PayType = syspaytype.PayType;
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
