using Microsoft.AspNet.Identity;
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
                var userId = User.Identity.GetUserId();
                Data.ActBuy newActBuy = new Data.ActBuy();

                newActBuy.PostId = add.PostId;
                newActBuy.BoughtByUserId = userId;
                newActBuy.BoughtDate = DateTime.Today;
                newActBuy.IsAccepted = false;
                newActBuy.Quantity = add.Quantity;
                newActBuy.PartialAmount = add.PartialAmount;
                db.ActBuys.InsertOnSubmit(newActBuy);
                db.SubmitChanges();

                var postItem = from d in db.ActPostItems
                               where d.Id == add.PostId
                               select d;

                if (postItem.Any())
                {
                    Data.ActMessaging messaging = new Data.ActMessaging();
                    messaging.SenderUserId = userId;
                    messaging.SenderMessageBody = add.Message + " - Requested Item: " + postItem.FirstOrDefault().StpItem.ItemName;
                    messaging.SenderMessageDate = DateTime.Today;
                    messaging.RecipientUserId = postItem.FirstOrDefault().PostedByUserId;
                    messaging.RecipientMessageBody = "";
                    messaging.RecipientMessageDate = DateTime.Today;
                    db.ActMessagings.InsertOnSubmit(messaging);
                    db.SubmitChanges();

                    Data.ActMessaging messagingReceiver = new Data.ActMessaging();
                    messagingReceiver.SenderUserId = postItem.FirstOrDefault().PostedByUserId;
                    messagingReceiver.SenderMessageBody = "";
                    messagingReceiver.SenderMessageDate = DateTime.Today;
                    messagingReceiver.RecipientUserId = userId;
                    messagingReceiver.RecipientMessageBody = add.Message + " - Requested Item: " + postItem.FirstOrDefault().StpItem.ItemName;
                    messagingReceiver.RecipientMessageDate = DateTime.Today;
                    db.ActMessagings.InsertOnSubmit(messagingReceiver);
                    db.SubmitChanges();
                }

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/inquirer")]
        public List<Entities.ActBuy> listActBuy()
        {
            var userId = User.Identity.GetUserId();
            var buy = from d in db.ActBuys
                      where d.ActPostItem.PostedByUserId == userId
                      && d.IsAccepted == false
                      select new Entities.ActBuy
                      {
                          Id = d.Id,
                          PostId = d.PostId,
                          Quantity = d.Quantity,
                          PartialAmount = d.PartialAmount,
                          BoughtByUser = d.AspNetUser.FullName,
                          BoughtDate = d.BoughtDate.ToShortDateString(),
                          IsAccepted = d.IsAccepted,
                          Inquirer = d.AspNetUser.FullName,
                          InquirerUserId = d.BoughtByUserId,
                          InquiredItem = d.ActPostItem.StpItem.ItemName
                        
                      };
            return buy.ToList();
        }


        [HttpGet, Route("api/list/unapproved/sold/{isaccepted}")]
        public List<Entities.ActBuy> listActBuy(String isaccepted)
        {
            var userId = User.Identity.GetUserId();
            var buy = from d in db.ActBuys
                      where d.ActPostItem.PostedByUserId == userId
                      && d.IsAccepted == Convert.ToBoolean(isaccepted)
                      select new Entities.ActBuy
                      {
                          Id = d.Id,
                          PostId = d.PostId,
                          Quantity = d.Quantity,
                          PartialAmount = d.PartialAmount,
                          BoughtByUser = d.AspNetUser.FullName,
                          BoughtDate = d.BoughtDate.ToShortDateString(),
                          IsAccepted = d.IsAccepted,
                          Inquirer = d.AspNetUser.FullName,
                          InquirerUserId = d.BoughtByUserId,
                          InquiredItem = d.ActPostItem.StpItem.ItemName

                      };
            return buy.ToList();
        }

        [HttpPut, Route("api/update/approved/{id}")]
        public HttpResponseMessage updateApproved(String id)
        {
            try
            {
                var actBuy = from d in db.ActBuys
                             where d.Id == Convert.ToInt32(id)
                             select d;

                if (actBuy.Any())
                {
                    var updateActBuy = actBuy.FirstOrDefault();
                    updateActBuy.IsAccepted = true;
                    db.SubmitChanges();

                    var postItem = from d in db.ActPostItems
                                   where d.Id == actBuy.FirstOrDefault().PostId
                                   select d;
                    
                    if(postItem.Any()){
                        var buyQuantity = postItem.FirstOrDefault().Quantity;
                        var updatePostItem = postItem.FirstOrDefault();
                        updatePostItem.Quantity = buyQuantity - actBuy.FirstOrDefault().Quantity;
                        db.SubmitChanges();
                    }

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
