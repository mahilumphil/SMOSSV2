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
                newActPostItem.PayTypeId = add.PayTypeId;
                newActPostItem.StatusId = add.StatusId;
                newActPostItem.IsApproved = false;


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
                           ItemName = d.StpItem.ItemName,
                           Specification = d.StpItem.Specification,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUserId = d.PostedByUserId,
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
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           ItemName = d.StpItem.ItemName,
                           Remarks = d.Remarks,
                           Price = d.StpItem.Price,
                           Specification = d.StpItem.Specification,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUserId = d.AspNetUser.FullName,
                           PayType = d.SysPayType.PayType,
                           Status = d.SysPostItemStatus.Status,
                           PayTypeId = d.PayTypeId,
                           StatusId = d.StatusId,
                           IsApproved = d.IsApproved,
                           PhotoValue = d.StpItem.Photo.ToArray()
                       };

            return post.ToList();
        }

        [HttpGet, Route("api/list/postdetail/{id}")]
        public Entities.ActPostItem postDetailById(String id)
        {
            var post = from d in db.ActPostItems
                       where d.Id == Convert.ToInt32(id)
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           ItemName = d.StpItem.ItemName,
                           Remarks = d.Remarks,
                           Price = d.StpItem.Price,
                           Specification = d.StpItem.Specification,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUserId = d.PostedByUserId,
                           PostedByUser = d.AspNetUser.FullName,
                           PayType = d.SysPayType.PayType,
                           Status = d.SysPostItemStatus.Status,
                           PayTypeId = d.PayTypeId,
                           StatusId = d.StatusId,
                           IsApproved = d.IsApproved,
                           PhotoValue = d.StpItem.Photo.ToArray()
                       };

            return (Entities.ActPostItem)post.FirstOrDefault();
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
                           ItemName = d.StpItem.ItemName,
                           Remarks = d.Remarks,
                           Price = d.StpItem.Price,
                           Specification = d.StpItem.Specification,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUserId = userId,
                           PayType = d.SysPayType.PayType,
                           Status = d.SysPostItemStatus.Status,
                           PayTypeId = d.PayTypeId,
                           StatusId = d.StatusId,
                           IsApproved = d.IsApproved,
                           PhotoValue = d.StpItem.Photo.ToArray()
                       };

            return post.ToList();
        }


        [HttpGet, Route("api/list/shoplist/{itemCategoryId}")]
        public List<Entities.ActPostItem> shopItemList(String itemCategoryId)
        {
            var post = from d in db.ActPostItems
                       where d.StpItem.ItemCategoryId == Convert.ToInt32(itemCategoryId)
                       select new Entities.ActPostItem
                       {
                           Id = d.Id,
                           ItemId = d.ItemId,
                           ItemName = d.StpItem.ItemName,
                           Remarks = d.Remarks,
                           Price = d.StpItem.Price,
                           Specification = d.StpItem.Specification,
                           PostDate = d.PostDate.ToShortDateString(),
                           ExpiredDate = d.ExpiredDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString(),
                           Quantity = d.Quantity,
                           PostedByUser = d.AspNetUser.FullName,
                           PayType = d.SysPayType.PayType,
                           Status = d.SysPostItemStatus.Status,
                           PayTypeId = d.PayTypeId,
                           StatusId = d.StatusId,
                           IsApproved = d.IsApproved,
                           PhotoValue = d.StpItem.Photo.ToArray()
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
                    updateActPostItem.ItemId= postitem.ItemId;
                    updateActPostItem.Remarks = postitem.Remarks;
                    updateActPostItem.PostDate = DateTime.Today;
                    updateActPostItem.ExpiredDate = DateTime.Today;
                    updateActPostItem.UpdatedDate = DateTime.Today;
                    updateActPostItem.Quantity = postitem.Quantity;
                    updateActPostItem.PayTypeId = postitem.PayTypeId;
                    updateActPostItem.StatusId = postitem.StatusId;
                    updateActPostItem.IsApproved = postitem.IsApproved;
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
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
