using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tenzat.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Tenzat.Controllers
{
    [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class ListsController : Controller
    {
        //
        // GET: /Lists/

        public ActionResult ViewList(int _ID)
        {
            List l = new List();
            l.ID = _ID;
            ViewBag.getlistitem = l.GetListItems().Data;
            TempData["listid"]=_ID;
            TempData.Keep();
            ViewBag.related = l.GetRelatedLists().Data;
            return View();
        }

        public JsonResult ListByID()
        {
            List l = new List();
            int _ID =(int)TempData["listid"];
            l.ID = _ID;
      return l.GetListByID().DataInJSON ;
        }
    }
}
