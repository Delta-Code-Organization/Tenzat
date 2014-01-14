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

        public ActionResult ViewList()
        {
            return View();
        }

        public Returner Confirm()
        {
            List l = new List();
            var res = l.ConfirmList(5);
            return res;
        }

        public Returner Remove()
        {
            List l = new List();
            var res = l.RemoveList(4);
            return res;
        }

    }
}
