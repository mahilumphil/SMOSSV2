using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiActPostItemController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/postitem")]
        public HttpResponseMessage addActPostItem(Entities.ActPostItem add)
        {
            try
            {
                Data.ActPostItem newActPostItem = new Data.ActPostItem();

                newActPostItem.Id = add.Id;
                newActPostItem.ItemId = add.ItemId;
                newActPostItem.PostDate = Convert.ToDateTime(add.PostDate);
                newActPostItem.ExpiredDate = Convert.ToDateTime(add.ExpiredDate);
                newActPostItem.UpdatedDate = Convert.ToDateTime(add.UpdatedDate);
                newActPostItem.Quantity = add.Quantity;
                newActPostItem.PostedByUserId = add.PostedByUserId;
                newActPostItem.IsLiked = add.IsLiked;
                newActPostItem.PayTypeId = add.PayTypeId;
                newActPostItem.StatusId = add.StatusId;    


                return Request.CreateResponse(HttpStatusCode.OK, "Sucessfully Added");

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        [HttpGet, Route("api/list/postitem")]
        public List<Entities.ActPostItem> listActPostItem()
        {
            var post = from d in db.ActPostItems
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUserId = d.PostedByUserId,
                           IsLiked = d.IsLiked,
                           PayTypeId = d.PayTypeId,
                           StatusId = d.StatusId
                       };

            return post.ToList();
        }


        [HttpDelete, Route("api/delete/postitem/{id}")]
        public HttpResponseMessage deleteActPostItem(String id)
        {
            try
            {
                var delActPostItem = from d in db.ActPostItems
                                     where d.Id == Convert.ToInt32(id)
                                     select d;

                if (delActPostItem.Any())
                {
                    db.ActPostItems.DeleteOnSubmit(delActPostItem.First());
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

        [HttpPut, Route("api/update/postitem/{id}")]
        public HttpResponseMessage updateActPostItem(String id, Entities.ActPostItem postitem)
        {
            try
            {
                var actPostItem = from d in db.ActPostItems
                                         where d.Id == Convert.ToInt32(id)
                                         select d;

                if (actPostItem.Any())
                {
                    var updateActPostItem = actPostItem.FirstOrDefault();
                    updateActPostItem.Id = postitem.Id;
                    updateActPostItem.ItemId = postitem.ItemId;
                    updateActPostItem.PostDate = Convert.ToDateTime(postitem.PostDate);
                    updateActPostItem.ExpiredDate = Convert.ToDateTime(postitem.ExpiredDate);
                    updateActPostItem.UpdatedDate = Convert.ToDateTime(postitem.UpdatedDate);
                    updateActPostItem.Quantity = postitem.Quantity;
                    updateActPostItem.PostedByUserId = postitem.PostedByUserId;
                    updateActPostItem.IsLiked = postitem.IsLiked;
                    updateActPostItem.PayTypeId = postitem.PayTypeId;
                    updateActPostItem.StatusId = postitem.StatusId;  
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
