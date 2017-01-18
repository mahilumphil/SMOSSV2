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
    public class ApiActPostItemController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/postitem")]
        public HttpResponseMessage addActPostItem(Entities.ActPostItem add)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                Data.ActPostItem newActPostItem = new Data.ActPostItem();

                newActPostItem.ItemId = add.ItemId;
                newActPostItem.Remarks = add.Remarks;
                newActPostItem.PostDate = DateTime.Today;
                newActPostItem.ExpiredDate = DateTime.Today.AddDays(30);
                newActPostItem.UpdatedDate = DateTime.Today;
                newActPostItem.Quantity = add.Quantity;
                newActPostItem.PostedByUserId = userId;
                newActPostItem.IsLiked = true;
                newActPostItem.PayTypeId = add.PayTypeId;
                newActPostItem.StatusId = add.StatusId;

                db.ActPostItems.InsertOnSubmit(newActPostItem);
                db.SubmitChanges();


                return Request.CreateResponse(HttpStatusCode.OK, "Sucessfully Added");

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }



        [HttpGet, Route("api/list/postitem/byuser")]
        public List<Entities.ActPostItem> listActPostItemByUser()
        {
            var userId = User.Identity.GetUserId();
            var post = from d in db.ActPostItems    
                       where d.PostedByUserId == userId
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           Item = d.StpItem.ItemName,
                           Specification = d.StpItem.Specification,
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


        [HttpGet, Route("api/list/postitem/byitemcategory/{itemcategoryid}/{startDate}/{endDate}")]
        public List<Entities.ActPostItem> listActPostItemAll(String itemcategoryid, String startDate,String endDate)
        {
            var post = from d in db.ActPostItems
                       where d.StpItem.ItemCategoryId == Convert.ToInt32(itemcategoryid)
                       && d.PostDate >= Convert.ToDateTime(startDate) && d.PostDate <= Convert.ToDateTime(endDate)
                       && d.ExpiredDate < DateTime.Today
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           Item = d.StpItem.ItemName,
                           Specification = d.StpItem.Specification,
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

        [HttpGet, Route("api/list/postlist")]
        public List<Entities.ActPostItem> postItemList()
        {
            var userId = User.Identity.GetUserId();
            var post = from d in db.ActPostItems
                       where d.PostedByUserId == userId
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           Item = d.StpItem.ItemName,
                           Remarks = d.Remarks,
                           Price = d.StpItem.Price,
                           Specification = d.StpItem.Specification,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUserId = userId,
                           IsLiked = d.IsLiked,
                           PayType = d.SysPayType.PayType,
                           Status = d.SysPostItemStatus.Status,
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
