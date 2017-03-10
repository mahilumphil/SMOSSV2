using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiActMessagingController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/messaging")]
        public HttpResponseMessage addActMessaging(Entities.ActMessaging add)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                // sender data message
                Data.ActMessaging newSenderActMessaging = new Data.ActMessaging();
                newSenderActMessaging.SenderUserId = userId;
                newSenderActMessaging.SenderMessageBody = add.MessageBody;
                newSenderActMessaging.SenderMessageDate = DateTime.Today;
                newSenderActMessaging.RecipientUserId = add.SenderUserId;
                newSenderActMessaging.RecipientMessageBody = "";
                newSenderActMessaging.RecipientMessageDate = DateTime.Today;
                db.ActMessagings.InsertOnSubmit(newSenderActMessaging);
                db.SubmitChanges();

                // recipient data message
                Data.ActMessaging newRecipientActMessaging = new Data.ActMessaging();
                newRecipientActMessaging.SenderUserId = add.SenderUserId;
                newRecipientActMessaging.SenderMessageBody = "";
                newRecipientActMessaging.SenderMessageDate = DateTime.Today;
                newRecipientActMessaging.RecipientUserId = userId;
                newRecipientActMessaging.RecipientMessageBody = add.MessageBody;
                newRecipientActMessaging.RecipientMessageDate = DateTime.Today;
                db.ActMessagings.InsertOnSubmit(newRecipientActMessaging);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/usermessages")]
        public List<Entities.ActMessaging> listActMessaging()
        {
            var userId = User.Identity.GetUserId();
            var message = from d in db.ActMessagings
                          where d.SenderUserId != userId
                          group d by new
                          {
                            SenderUserId = d.SenderUserId,
                            SenderUser = d.AspNetUser.FullName
                          }into g
                          select new Entities.ActMessaging
                          {
                             SenderUserId = g.Key.SenderUserId,
                             SenderUser = g.Key.SenderUser
                          };
            return message.ToList();
        }

        [HttpGet, Route("api/list/message/{senderuserid}")]
        public List<Entities.ActMessaging> listActMessaging(String senderUserId)
        {
            var userId = User.Identity.GetUserId();
            var message = from d in db.ActMessagings
                          where d.SenderUserId == userId
                          && d.RecipientUserId == senderUserId
                          select new Entities.ActMessaging
                          {
                                SenderUserId = d.SenderUserId,
                                SenderUser = d.AspNetUser.FullName,
                                SenderMessageBody = d.SenderMessageBody,
                                SenderMessageDate = d.SenderMessageDate.ToShortDateString(),
                                RecipientUserId = d.RecipientUserId,
                                RecipientUser = d.AspNetUser1.FullName,
                                RecipientMessageBody = d.RecipientMessageBody,
                                RecipientMessageDate = d.RecipientMessageDate.ToShortDateString()
                          };
            return message.ToList();
        }


        [HttpDelete, Route("api/delete/messaging/{id}")]
        public HttpResponseMessage deleteActMessaging(String id)
        {
            try
            {
                var delActMessaging = from d in db.ActMessagings
                                      where d.Id == Convert.ToInt32(id)
                                      select d;

                if (delActMessaging.Any())
                {
                    db.ActMessagings.DeleteOnSubmit(delActMessaging.First());
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



        [HttpPut, Route("api/update/messaging/{id}")]
        public HttpResponseMessage updateActMessaging(String id, Entities.ActMessaging messaging)
        {
            try
            {
                var actMessaging = from d in db.ActMessagings
                             where d.Id == Convert.ToInt32(id)
                             select d;

                if (actMessaging.Any())
                {
                    var updateActMessaging = actMessaging.FirstOrDefault();
                    updateActMessaging.Id = messaging.Id;
                    updateActMessaging.SenderUserId = messaging.SenderUserId;
                    updateActMessaging.RecipientUserId = messaging.RecipientUserId;
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
