using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Linq;


namespace SmartShopWebApp.ApiControllers
{
    public class ApiStpItemController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/stpitem")]
        public HttpResponseMessage newStpItem(Entities.StpItem add)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                byte[] imageArray = add.Photo;

                Data.StpItem newStpItem = new Data.StpItem();
                newStpItem.Photo = new Binary(imageArray);
                newStpItem.ItemName = add.ItemName;
                newStpItem.Price = add.Price;
                newStpItem.ItemCategoryId = add.ItemCategoryId;
                newStpItem.Specification = add.Specification;
                newStpItem.UserId = userId;
                newStpItem.CreatedDate = DateTime.Today;
                newStpItem.UpdatedDate = DateTime.Today;
                db.StpItems.InsertOnSubmit(newStpItem);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        [HttpGet, Route("api/list/stpitem")]
        public List<Entities.StpItem> listStpItem()
        {
            var userId = User.Identity.GetUserId();
            var item = from d in db.StpItems
                       where d.UserId == userId
                       select new Entities.StpItem
                       {
                           Id = d.Id,
                           Photo = d.Photo.ToArray(),
                           ItemName = d.ItemName,
                           Price = d.Price,
                           ItemCategoryId = d.ItemCategoryId,
                           ItemCategory = d.SysItemCategory.ItemCategory,
                           Specification = d.Specification,
                           UserId = d.UserId,
                           CreatedDate = d.CreatedDate.ToShortDateString(),
                           UpdatedDate = d.UpdatedDate.ToShortDateString()
                       };
            return item.ToList();
        }


        [HttpDelete, Route("api/delete/stpitem/{id}")]
        public HttpResponseMessage deleteStpItem(String id)
        {
            try
            {
                var delStpItem = from d in db.StpItems
                                 where d.Id == Convert.ToInt32(id)
                                 select d;

                if (delStpItem.Any())
                {
                    db.StpItems.DeleteOnSubmit(delStpItem.First());
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


        [HttpPut, Route("api/update/stpitem/{id}")]
        public HttpResponseMessage updateStpItem(String id, Entities.StpItem stpitem)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var actStpItem = from d in db.StpItems
                                 where d.Id == Convert.ToInt32(id)
                                 select d;

                if (actStpItem.Any())
                {
                    byte[] imageArray = stpitem.Photo;
                    var updateStpItem = actStpItem.FirstOrDefault();
                    updateStpItem.Photo = new Binary(imageArray);
                    updateStpItem.ItemName = stpitem.ItemName;
                    updateStpItem.Price = stpitem.Price;
                    updateStpItem.ItemCategoryId = stpitem.ItemCategoryId;
                    updateStpItem.Specification = stpitem.Specification;
                    updateStpItem.UserId = userId;
                    updateStpItem.UpdatedDate = DateTime.Today;
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
