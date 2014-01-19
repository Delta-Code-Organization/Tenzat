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
           // string[] Tags = Enum.GetNames(typeof(Tags));

            //CreatList();
           //CreatModerator();
         //  LOgin();
            //SetItem();
            //SetHot();
            //Remove();
           // ListById();
          //  ModifyAdmin();
            return View();
        }

        public JsonResult Date()
        {
            List l = new List();
            return Json(l.GetListByDAte());
        }
        public JsonResult Rank()
        {
            List l = new List();
            return Json(l.GetListByRank());
        }

        public Returner Title()
        {
            List l = new List();
          var res=l.SearchByTitle("لحظات ");
          return res;
        }

        public Returner Tag()
        {
            List l = new List();
            var res = l.SearchByTag(1);
            return res;
        }

        //public Returner List()
        //{
        //    List l = new List();
        //    var res = l.GetList();
        //    return res;
        //}

        public Returner CreatList()
        {
            List l = new List();
            l.image = "fadzjhghl/hkaj";
            //l.ListDate = DateTime.Now;
            l.ListType = 4;
           // l.Rank = 9;
            l.Status = 2;
           // l.FbShares = 500;
           // l.TwitterShares = 200;
           // l.Views = 1000;
            l.Title = "الاهرام";
            l.Tag = 2;
            l.Hot = false;
            l.CreatedBy = 1;
            var res = l.CreateList();
            return res;
        }

        public Returner ListById()
        {
            List l = new List();
            var res = l.GetListByID(4);
            return res;
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

        public Returner SetItem()
        {
            List L = new List();
            ListItem LI = new ListItem();
            L.ID = 4;
            LI.Drawable = "video";
            LI.Description = "اغرب 10 شخصيات في العالم";
            LI.ItemType = 0;
            LI.MoreText = "سام من اغرب 10 شخصيات في الكون";
            LI.Order = 1;
            LI.Title = "dhc;l";
          var res=L.SetListItem(LI);
          return res;
        }

        //public Returner LOgin()
        //{
        //    Moderator moder = new Moderator();
        // var res=moder.ModeratorLogin("delta@yahoo.com","26990an");
        // return res;
        //}

        public Returner CreatModerator()
        {
            Moderator moder = new Moderator();
            moder.password = "254hgh";
            moder.Email = "code@yahoo.com";
            moder.CreateAdmin= true;
            moder.CreateList = true;
            moder.SetHotLists = true;
            moder.Status = 1;
            var res = moder.CreateModerator();
            return res;
        }
    }
}
