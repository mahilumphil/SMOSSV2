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
                Data.ActMessaging newActMessaging = new Data.ActMessaging();
                newActMessaging.SenderUserId = userId;
                newActMessaging.RecipientUserId = add.SenderUserId;
                newActMessaging.IsOpen = true;
                newActMessaging.MessageBody = add.MessageBody;
                newActMessaging.MessageDate = DateTime.Today;
                newActMessaging.MessageForFirstUserId = userId;
                newActMessaging.MessageForSecondUserId = add.SenderUserId;
                db.ActMessagings.InsertOnSubmit(newActMessaging);
                db.SubmitChanges();

                // receiver data message
                Data.ActMessaging newActMessaging2 = new Data.ActMessaging();
                newActMessaging2.SenderUserId = add.SenderUserId;
                newActMessaging2.RecipientUserId = userId;
                newActMessaging2.IsOpen = true;
                newActMessaging2.MessageBody = add.MessageBody;
                newActMessaging2.MessageDate = DateTime.Today;
                newActMessaging2.MessageForFirstUserId = userId;
                newActMessaging2.MessageForSecondUserId = add.SenderUserId;
                db.ActMessagings.InsertOnSubmit(newActMessaging2);
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
            var message = from d in db.ActMessagings.OrderByDescending(d => d.IsOpen)
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
                          where d.MessageForFirstUserId == userId
                          && d.MessageForSecondUserId == senderUserId
                          select new Entities.ActMessaging
                          {
                                SenderUserId = d.SenderUserId,
                                SenderUser = d.AspNetUser.FullName,
                                RecipientUser = d.AspNetUser1.FullName,
                                MessageBody = d.MessageBody,
                                MessageDate = d.MessageDate.ToShortDateString()
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
                    updateActMessaging.MessageBody = messaging.MessageBody;
                    updateActMessaging.MessageDate = Convert.ToDateTime(messaging.MessageDate);
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
