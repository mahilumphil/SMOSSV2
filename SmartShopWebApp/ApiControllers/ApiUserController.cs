using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.Data.Linq;
using System.Diagnostics;

namespace SmartShopWebApp.ApiControllers
{
    public class ApiUserController : ApiController
    {
        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        [HttpPost, Route("api/add/userprofile")]
        public HttpResponseMessage newUserProfile(Entities.UserProfile add)
        {
            try
            {
                byte[] imageArray = add.ProfilePicture;

                Data.AspNetUser newUserProfile = new Data.AspNetUser();
                newUserProfile.ProfilePhoto = new Binary(imageArray);
                db.AspNetUsers.InsertOnSubmit(newUserProfile);
                db.SubmitChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        [HttpGet, Route("api/list/user")]
        public List<Entities.UserProfile> listUser()
        {
            var userId = User.Identity.GetUserId();
            var user = from d in db.AspNetUsers
                       where d.Id == userId
                       select new Entities.UserProfile                       
                       {
                           Email = d.Email,
                           FullName = d.FullName,
                           ContactNumber = d.ContactNumber,
                           Address = d.Address,
                           Type = d.Type
                       };
            return user.ToList();
        }

    }
}
