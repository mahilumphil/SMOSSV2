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
                newActPostItem.Discount = add.Discount;
                newActPostItem.IsApproved = false;
                newActPostItem.ItemView = null; 
                newActPostItem.StatusRate1 = null;
                newActPostItem.StatusRate2 = null;
                newActPostItem.StatusRate3 = null;
                newActPostItem.StatusRate4 = null;
                newActPostItem.StatusRate5 = null;

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
                           StatusId = d.StatusId,
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
                       };

            return post.ToList();
        }


        [HttpGet, Route("api/list/postitem/byitemcategory/{itemcategoryid}/{startDate}/{endDate}")]
        public List<Entities.ActPostItem> listActPostItemAll(String itemcategoryid, String startDate, String endDate)
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
                           PhotoValue = d.StpItem.Photo.ToArray(),
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
                       };

            return post.ToList();
        }

        [HttpGet, Route("api/list/allitem")]
        public List<Entities.ActPostItem> listActPostItemAll()
        {
            var post = from d in db.ActPostItems
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
                           PhotoValue = d.StpItem.Photo.ToArray(),
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
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
                           PhotoValue = d.StpItem.Photo.ToArray(),
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
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
                           PhotoValue = d.StpItem.Photo.ToArray(),
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
                       };

            return post.ToList();
        }

        [HttpGet, Route("api/list/postlist/index")]
        public List<Entities.ActPostItem> postItemListIndex()
        {
            var post = from d in db.ActPostItems.OrderByDescending(d => d.Id)
                       where d.Quantity > 0
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
                           Discount = d.Discount,
                           PayTypeId = d.PayTypeId,
                           StatusId = d.StatusId,
                           IsApproved = d.IsApproved,
                           PhotoValue = d.StpItem.Photo.ToArray(),
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
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
                           PhotoValue = d.StpItem.Photo.ToArray(),
                           StatusRate1 = d.StatusRate1,
                           StatusRate2 = d.StatusRate2,
                           StatusRate3 = d.StatusRate3,
                           StatusRate4 = d.StatusRate4,
                           StatusRate5 = d.StatusRate5,
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
                    updateActPostItem.ItemId = postitem.ItemId;
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





        [HttpPut, Route("api/update/postitem/{id}/{itemview}")]
        public HttpResponseMessage itemView(String id, String itemview)
        {
            try
            {
                var actPostItem = from d in db.ActPostItems
                                  where d.Id == Convert.ToInt32(id)
                                  select d;

                if (actPostItem.Any())
                {
                    var updateActPostItem = actPostItem.FirstOrDefault();

                    if (Convert.ToInt32(itemview) == 1)
                    {
                        Decimal? itemView = 0;
                        if (actPostItem.FirstOrDefault().ItemView != null) {
                            itemView = actPostItem.FirstOrDefault().ItemView;
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        updateActPostItem.ItemView = itemView + 1;
                        db.SubmitChanges();
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }

                    return Request.CreateResponse(HttpStatusCode.BadRequest);
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



        [HttpPut, Route("api/update/updateRate/{id}/{ratenumber}")]
        public HttpResponseMessage updateRate(String id, String ratenumber)
        {
            try
            {
                var actPostItem = from d in db.ActPostItems
                                  where d.Id == Convert.ToInt32(id)
                                  select d;

                if (actPostItem.Any())
                {
                    var updateActPostItem = actPostItem.FirstOrDefault();
                    

                    if (Convert.ToInt32(ratenumber) == 1)
                    {
                        Decimal? getRate1 = 0;
                        if (actPostItem.FirstOrDefault().StatusRate1 != null) {
                            getRate1 = actPostItem.FirstOrDefault().StatusRate1;
                        }
                        updateActPostItem.StatusRate1 = getRate1 + 1;
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else if (Convert.ToInt32(ratenumber) == 2)
                    {
                        Decimal? getRate2 = 0;
                        if (actPostItem.FirstOrDefault().StatusRate2 != null) {
                            getRate2 = actPostItem.FirstOrDefault().StatusRate2;
                        }
                        updateActPostItem.StatusRate2 = getRate2 + 1;
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else if (Convert.ToInt32(ratenumber) == 3)
                    {
                        Decimal? getRate3 = 0;
                        if (actPostItem.FirstOrDefault().StatusRate3 != null) {
                            getRate3 = actPostItem.FirstOrDefault().StatusRate3;
                        }
                        updateActPostItem.StatusRate3 = getRate3 + 1;
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else if (Convert.ToInt32(ratenumber) == 4)
                    {
                        Decimal? getRate4 = 0;
                        if(actPostItem.FirstOrDefault().StatusRate4 != null){
                            getRate4 = actPostItem.FirstOrDefault().StatusRate4;
                        }
                        updateActPostItem.StatusRate4 = getRate4 + 1;
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else if (Convert.ToInt32(ratenumber) == 5)
                    {
                        Decimal? getRate5 = 0;
                        if(actPostItem.FirstOrDefault().StatusRate5 != null) {
                            getRate5 = actPostItem.FirstOrDefault().StatusRate5;
                        }
                        updateActPostItem.StatusRate5 = getRate5 + 1;
                        db.SubmitChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest);
                    }
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
