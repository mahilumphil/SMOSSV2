using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
                Data.ActMessaging newActMessaging = new Data.ActMessaging();

                newActMessaging.Id = add.Id;
                newActMessaging.SenderUserId = add.SenderUserId;
                newActMessaging.RecipientUserId = add.RecipientUserId;
                newActMessaging.MessageBody = add.MessageBody;
                newActMessaging.MessageDate = Convert.ToDateTime(add.MessageDate);


                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/messaging")]
        public List<Entities.ActMessaging> listActMessaging()
        {
            var message = from d in db.ActMessagings
                          select new Entities.ActMessaging
                          {
                              Id = d.Id,
                              SenderUserId = d.SenderUserId,
                              RecipientUserId = d.RecipientUserId,
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
