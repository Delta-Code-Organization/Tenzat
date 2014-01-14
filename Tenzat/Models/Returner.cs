using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tenzat.Models
{
    public class Returner
    {
        public Msgs Message { get; set; }
        public object Data { get; set; }
        public JsonResult DataInJSON { get; set; }
    }
}