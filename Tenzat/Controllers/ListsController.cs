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
            var res = l.GetListItems(_ID).Data;
            TempData["listid"]=_ID;
            TempData.Keep();
            ViewBag.getlistitem = res;
            ViewBag.related = l.GetRelatedLists(_ID).Data;
            return View();
        }

        public JsonResult ListByID()
        {
            List l = new List();
            int _ID =(int)TempData["listid"];
      return l.GetListByID(_ID).DataInJSON ;
        }
    }
}
