using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}