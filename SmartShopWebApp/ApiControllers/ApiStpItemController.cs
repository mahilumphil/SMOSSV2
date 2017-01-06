using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
                Data.StpItem newStpItem = new Data.StpItem();

                newStpItem.Id = add.Id;
                newStpItem.Photo = add.Photo;
                newStpItem.ItemName = add.ItemName;
                newStpItem.Price = add.Price;
                newStpItem.ItemCategoryId = add.ItemCategoryId;
                newStpItem.Specification = add.Specification;
                newStpItem.UserId = add.UserId;
                newStpItem.CreatedDate = Convert.ToDateTime(add.CreatedDate);
                newStpItem.UpdatedDate = Convert.ToDateTime(add.UpdatedDate);


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
            var item = from d in db.StpItems
                       select new Entities.StpItem
                       {
                           Id = d.Id,
                           //Photo = d.Photo,
                           ItemName = d.ItemName,
                           Price = d.Price,
                           ItemCategoryId = d.ItemCategoryId,
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
                var actStpItem = from d in db.StpItems
                                  where d.Id == Convert.ToInt32(id)
                                  select d;

                if (actStpItem.Any())
                {
                    var updateStpItem = actStpItem.FirstOrDefault();
                    updateStpItem.Id = stpitem.Id;
                    updateStpItem.Photo = stpitem.Photo;
                    updateStpItem.ItemName = stpitem.ItemName;
                    updateStpItem.Price = stpitem.Price;
                    updateStpItem.ItemCategoryId = stpitem.ItemCategoryId;
                    updateStpItem.Specification = stpitem.Specification;
                    updateStpItem.UserId = stpitem.UserId;
                    updateStpItem.CreatedDate = Convert.ToDateTime(stpitem.CreatedDate);
                    updateStpItem.UpdatedDate = Convert.ToDateTime(stpitem.UpdatedDate);
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
