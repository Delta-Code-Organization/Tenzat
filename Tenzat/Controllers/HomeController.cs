using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tenzat.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Tenzat.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            List l = new List();
            ViewBag.Ranklists = (l.GetListByDAte().Data);
            List<ListItem> LOL = new List<ListItem>();
            List<int> IDS = new List<int>();
            foreach (List item in ViewBag.Ranklists)
            {
                int ID = item.ID;
                LOL.AddRange(item.ListItems);
                IDS.Add(ID);
            }
            ViewBag.ListItems = LOL;
            TempData["IDS"] = IDS;
            TempData.Keep();
            return View();
        }

        public JsonResult GetLatest()
        {
            List L = new List();
            List<List> LOL = new List<List>();
            List<int> IDS = TempData["IDS"] as List<int>;
            Returner R = new Returner();
            R = L.GetListByRankAjax(IDS);
            LOL = R.Data as List<List>;
            foreach (var item in LOL)
            {
                int ID = item.ID;
                IDS.Add(ID);
            }
            TempData["IDS"] = IDS;
            TempData.Keep();
            return R.DataInJSON;
        }

        public JsonResult GetHotList()
        {
            return new List().GetHotList().DataInJSON;
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public void SendMessage(FormCollection FC)
        {
            Helper H = new Helper();
            H.SendEmail(FC["name"], "info@tenzat.com", "Email: " + FC["mail"] + "<br />" + "Message: " + FC["comment"]);
        }
    }
}
