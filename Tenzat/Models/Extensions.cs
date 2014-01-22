using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tenzat.Models
{
    public static class Extensions
    {
        public static string ShowMsg(this Msgs Enum)
        {
            return Enum.ToString().Replace("_", " ");
        }

        public static JsonResult ToJSON(this object Obj)
        {
            JsonResult JR = new JsonResult {
                Data = Obj,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return JR;
        }
      
    }
}