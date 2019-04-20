using MVC_CircloidTemplate.App_Classes;
using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.OnlineUserCount = HttpContext.Application["OnlineUserCount"];
            ViewBag.TotalUserCount = HttpContext.Application["TotalUserCount"];
            return View();
        }
        public ActionResult CookieAta()
        {
            //Cookie'ye değer atamayı sağlayacak.
            return View();
        }
        [HttpPost]
        public ActionResult CookieAta(string CookieName, string CookieValue)
        {
            HttpCookie ck = new HttpCookie(CookieName);
            ck.Value = CookieValue;
            ck.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(ck);
            return View();
        }
        public ActionResult CookieOku()
        {
            string cv = Request.Cookies["CS302-Afternoon"].Value;
            ViewBag.CookieSample = cv;
            return View();
        }
        public ActionResult MyCartList()
        {
            if (Session["CurrentCart"] != null)
            {
                UserCart uc = (UserCart)Session["CurrentCart"];
                return View(uc.PrdList);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public string RemoveFromCart(int id)
        {
            string messageRemoveFromCart = "";
            UserCart uc = (UserCart)Session["CurrentCart"];
            foreach (Product prod in uc.PrdList)
            {
                if (prod.ProductID == id)
                {
                    uc.PrdList.Remove(prod);
                    Session["CurrentCart"] = uc;
                    messageRemoveFromCart = "Ürün Sepetten çıkarıldı";
                    //return messageRemoveFromCart;
                    break;
                }
            }
            //messageRemoveFromCart = "hata";
            return messageRemoveFromCart;
        }
       public ActionResult PartialCartListView()
        {
            UserCart uc = (UserCart)Session["CurrentCart"];
            //int n = uc.PrdList.Count();
            return PartialView(uc.PrdList);
        }
    }
}