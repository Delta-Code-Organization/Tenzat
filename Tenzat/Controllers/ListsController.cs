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

    }
}
