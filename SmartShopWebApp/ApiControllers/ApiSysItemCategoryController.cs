using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiSysItemCategoryController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/sysitemcategory")]
        public HttpResponseMessage addSysItemCategory(Entities.SysItemCategory add)
        {
            try
            {
                Data.SysItemCategory newSysItemCategory = new Data.SysItemCategory();

                newSysItemCategory.Id = add.Id;
                newSysItemCategory.ItemCategory = add.ItemCategory;

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        [HttpGet, Route("api/list/sysitemcategory")]
        public List<Entities.SysItemCategory> listSysItemCategory()
        {
            var itemCategory = from d in db.SysItemCategories
                               select new Entities.SysItemCategory
                               {
                                   Id = d.Id,
                                   ItemCategory = d.ItemCategory
                               };
            return itemCategory.ToList();
        }


        [HttpDelete, Route("api/delete/sysitemcategory/{id}")]
        public HttpResponseMessage deleteSysItemCategory(String id)
        {
            try
            {
                var delSysItemCategory = from d in db.SysItemCategories
                                         where d.Id == Convert.ToInt32(id)
                                         select d;

                if (delSysItemCategory.Any())
                {
                    db.SysItemCategories.DeleteOnSubmit(delSysItemCategory.First());
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

        [HttpPut, Route("api/update/sysitemcategory/{id}")]
        public HttpResponseMessage updateSysItemCategory(String id, Entities.SysItemCategory sysitemcategory)
        {
            try
            {
                var actSysItemCategory = from d in db.SysItemCategories
                                         where d.Id == Convert.ToInt32(id)
                                         select d;

                if (actSysItemCategory.Any())
                {
                    var updateSysItemCategory = actSysItemCategory.FirstOrDefault();
                    updateSysItemCategory.Id = sysitemcategory.Id;
                    updateSysItemCategory.ItemCategory = sysitemcategory.ItemCategory;
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
