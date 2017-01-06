using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiActBuyController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/buy")]
        public HttpResponseMessage addActBuy(Entities.ActBuy add)
        {
            try
            {
                Data.ActBuy newActBuy = new Data.ActBuy();

                newActBuy.Id = add.Id;
                newActBuy.PostId = add.PostId;
                newActBuy.BoughtByUserId = add.BoughtByUserId;
                newActBuy.BoughtDate = Convert.ToDateTime(add.BoughtDate);
                newActBuy.IsAccepted = add.IsAccepted;

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/buy")]
        public List<Entities.ActBuy> listActBuy()
        {
            var buy = from d in db.ActBuys
                      select new Entities.ActBuy
                      {
                          Id = d.Id,
                          PostId = d.PostId,
                          BoughtByUserId = d.BoughtByUserId,
                          BoughtDate = d.BoughtDate.ToShortDateString(),
                          IsAccepted = d.IsAccepted
                      };
            return buy.ToList();
        }

        [HttpDelete, Route("api/delete/buy/{id}")]
        public HttpResponseMessage deleteActBuy(String id)
        {
            try
            {
                var delActBuy = from d in db.ActBuys
                                where d.Id == Convert.ToInt32(id)
                                select d;

                if (delActBuy.Any())
                {
                    db.ActBuys.DeleteOnSubmit(delActBuy.First());
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

        [HttpPut, Route("api/update/buy/{id}")]
        public HttpResponseMessage updateActBuy(String id, Entities.ActBuy buy)
        {
            try
            {
                var actBuy = from d in db.ActBuys
                             where d.Id == Convert.ToInt32(id)
                             select d;

                if (actBuy.Any())
                {
                    var updateActBuy = actBuy.FirstOrDefault();
                    updateActBuy.Id = buy.Id;
                    updateActBuy.PostId = buy.PostId;
                    updateActBuy.BoughtByUserId = buy.BoughtByUserId;
                    updateActBuy.BoughtDate = Convert.ToDateTime(buy.BoughtDate);
                    updateActBuy.IsAccepted = buy.IsAccepted;
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
