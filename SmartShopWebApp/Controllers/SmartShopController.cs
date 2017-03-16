using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SmartShopWebApp.Controllers
{
    public class SmartShopController : Controller
    {
        // GET: SmartShop
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Item()
        {
            return View();
        }

        [Authorize]
        public ActionResult Post()
        {
            return View();
        }

        [Authorize]
        public ActionResult PostList()
        {
            return View();
        }

        [Authorize]
        public ActionResult PostDetail()
        {
            return View();
        }

        [Authorize]
        public ActionResult ShopList()
        {
            return View();
        }

        [Authorize]
        public ActionResult Messaging()
        {
            return View();
        }

        [Authorize]
        public ActionResult Inquirers()
        {
            return View();
        }

        public ActionResult Maps()
        {
            return View();
        }

        public ActionResult ForeBiddenPage()
        {
            return View();
        }

        private Data.smartshopdbDataContext db = new Data.smartshopdbDataContext();

        public ActionResult AdminPanel()
        {
            var currentUser = User.Identity.GetUserId();
            var aspNetUser = from d in db.AspNetUsers
                             where d.Id == currentUser
                             select d;
            if (aspNetUser.Any())
            {
                if (aspNetUser.FirstOrDefault().RoleNumber != 1)
                {
                    return RedirectToAction("ForeBiddenPage", "SmartShop");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
    }
}