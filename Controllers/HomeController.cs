using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using Qiliqid.V1.Models;

namespace Qiliqid.V1.Controllers
{
    public class HomeController : Controller
    {
        private BDD db = new BDD();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpdateData()
        {
            QiBot bot = new QiBot
            {
                DomaineName = "https://www.supermarches.ca",
                Node = "//*[@id='table28']/tr[2]/td/div/table/tr",
            };

            bot.DropTableArticles();

            int lastPageNumber = 208;

            for (int i = 1; i <= lastPageNumber; i++)
            {
                HtmlNodeCollection res = bot.GetDataFromSource("https://www.supermarches.ca/pages/Default.asp?page=" + i.ToString());
                bot.PutIntoBDD(res);
            }

            return RedirectToAction("Index", "Articles");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}