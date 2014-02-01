using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Tenzat.Models;
using System.Web.Script.Serialization;

namespace Tenzat.Controllers
{
    public class Videos
    {
        public int Order { get; set; }
        public string URL { get; set; }
    }

    public class ModeratorController : Controller
    {
        //
        // GET: /Moderator/

        public ActionResult Login()
        {
            return View();
        }


        public string logadmin(string _mail, string _pass)
        {
            Moderator mod = new Moderator();
            mod.Email = _mail;
            mod.password = _pass;
            var res = mod.ModeratorLogin();
            var msgres = res.Message.ShowMsg();
            var nmod = res.Data;
            mod = (Moderator)nmod;
            if (msgres != "Username_Or_Password_Is_Incorrect")
            {
                Session.Clear();
                Session["moderator"] = mod;
                return msgres;
            }
            return msgres;
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login", "Moderator");
        }

        public ActionResult ControlPanel()
        {
            if (Session["moderator"] == null)
            {
                return RedirectToAction("Login", "Moderator");
            }
            ViewBag.Mod = Session["moderator"] as Moderator;
            return View();
        }

        public ActionResult ModifyAdmin()
        {
            if (Session["moderator"] == null)
            {
                return RedirectToAction("Login", "Moderator");
            }
            Moderator MM = new Moderator();
            MM = Session["moderator"] as Moderator;
            if (MM.CreateAdmin != true)
            {
                return RedirectToAction("ControlPanel", "Moderator");
            }
            Moderator M = new Moderator();
            List<Moderator> LOM = new List<Moderator>();
            LOM = M.allmod();
            ViewBag.Moderators = LOM;
            return View();
        }

        public ActionResult CreateList()
        {
            if (Session["moderator"] == null)
            {
                return RedirectToAction("Login", "Moderator");
            }
            Moderator MM = new Moderator();
            MM = Session["moderator"] as Moderator;
            if (MM.CreateList != true)
            {
                return RedirectToAction("ControlPanel", "Moderator");
            }
            string[] Tags = Enum.GetNames(typeof(Tags));
            ViewBag.Tags = Tags;
            return View();
        }

        public string AddList(string ListTitle, string listtype, string ImageURL, string VideoURL, int Tag, string[] ListItems, string VideosUrls)
        {
            JavaScriptSerializer JSS = new JavaScriptSerializer();
            Videos[] ArrayOfVideos = JSS.Deserialize<Videos[]>(VideosUrls);
            List<ListItem> ImagePathes = new List<ListItem>();
            List<ListItem> LOLI = new List<ListItem>();
            if (listtype == "Img")
            {
                ImagePathes = TempData["ImagePathes"] as List<ListItem>;
            }
            int skip = 0;
            for (int i = 1; i <= 10; i++)
            {
                List<string> LOS = ListItems.Skip(skip).Take(2).Cast<string>().ToList();
                ListItem LI = new ListItem();
                LI.Description = LOS[1];
                if (skip == 0)
                {
                    LI.Order = 1;
                }
                else
                {
                    LI.Order = (skip / 2) + 1;
                }
                bool ImageOrVideo = ImagePathes.Any(p => p.Order == LI.Order);
                if (ImageOrVideo)
                {
                    LI.Drawable = ImagePathes.Where(p => p.Order == LI.Order).SingleOrDefault().Drawable;
                    LI.ItemType = (int)ItemType.Image;
                }
                else
                {
                    LI.Drawable = ArrayOfVideos.Where(p => p.Order == LI.Order).SingleOrDefault().URL;
                    LI.ItemType = (int)ItemType.Video;
                }
                LI.Title = LOS.FirstOrDefault();
                LOLI.Add(LI);
                skip += 2;
            }
            List L = new List();
            L.CreatedBy = (Session["moderator"] as Moderator).ID;
            L.FbShares = 0;
            L.Hot = false;
            string Path;
            if (ImageURL != null)
            {
                Image Img = LoadImage(ImageURL);
                using (Bitmap image = new Bitmap(Img))
                {
                    var fileName = Guid.NewGuid().ToString() + ".png";
                    Path = @"/content/imgs/Lists/" + fileName;
                    string ImagePath = Server.MapPath(Path);
                    image.Save(ImagePath);
                }
                L.image = Path;
                L.ListType = (int)ItemType.Image;
            }
            else
            {
                L.image = VideoURL;
                L.ListType = (int)ItemType.Video;
            }
            L.ListDate = DateTime.Now;
            L.ListItems = LOLI;
            L.Rank = 0;
            L.Status = 0;
            L.Tag = Tag;
            L.Title = ListTitle;
            L.TwitterShares = 0;
            L.Views = 0;
            return L.CreateList().Message.ShowMsg();
        }

        private Image LoadImage(string Base64)
        {
            //get a temp image from bytes, instead of loading from disk
            //data:image/gif;base64,
            //this image is a single pixel (black)
            byte[] bytes = Convert.FromBase64String(Base64);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        public ActionResult CreateAdmin()
        {
            if (Session["moderator"] == null)
            {
                return RedirectToAction("Login", "Moderator");
            }
            Moderator MM = new Moderator();
            MM = Session["moderator"] as Moderator;
            if (MM.CreateAdmin != true)
            {
                return RedirectToAction("ControlPanel", "Moderator");
            }
            return View();
        }

        public string CreatModerator(string _email, string _paass, Boolean _manage, Boolean _create, Boolean _hot, Boolean _set)
        {
            Moderator moder = new Moderator();
            moder.Email = _email;
            moder.password = _paass;
            moder.CreateAdmin = _manage;
            moder.CreateList = _create;
            moder.SetHotLists = _hot;
            moder.ApproveLists = _set;
            moder.Status = 1;
            var res = (moder.CreateModerator()).Message.ShowMsg();
            return res;
        }

        public ActionResult SearchList()
        {
            if (Session["moderator"] == null)
            {
                return RedirectToAction("Login", "Moderator");
            }
            Moderator MM = new Moderator();
            MM = Session["moderator"] as Moderator;
            if (MM.SetHotLists != true && MM.ApproveLists != true)
            {
                return RedirectToAction("ControlPanel", "Moderator");
            }
            string[] Tags = Enum.GetNames(typeof(Tags));
            ViewBag.Tags = Tags;
            return View();
        }

        public string UploadImage(string ImageBase64, int orderid)
        {
            string Path = "";
            if (ImageBase64 != null)
            {
                Image Img = LoadImage(ImageBase64);
                using (Bitmap image = new Bitmap(Img))
                {
                    var fileName = Guid.NewGuid().ToString() + ".png";
                    Path = @"/content/imgs/Lists/" + fileName;
                    string ImagePath = Server.MapPath(Path);
                    image.Save(ImagePath);
                }
            }
            if (TempData["ImagePathes"] != null)
            {
                List<ListItem> LOLI = new List<ListItem>();
                LOLI = TempData["ImagePathes"] as List<ListItem>;
                ListItem LI = new ListItem();
                LI.Order = orderid;
                LI.Drawable = Path;
                if (LOLI.Any(p => p.Order == orderid))
                {
                    ListItem OldList = new ListItem();
                    OldList = LOLI.Where(p => p.Order == orderid).SingleOrDefault();
                    LOLI.Remove(OldList);
                }
                LOLI.Add(LI);
                TempData["ImagePathes"] = LOLI;
                TempData.Keep();
            }
            else
            {
                List<ListItem> LOLI = new List<ListItem>();
                ListItem LI = new ListItem();
                LI.Order = orderid;
                LI.Drawable = Path;
                LOLI.Add(LI);
                TempData["ImagePathes"] = LOLI;
                TempData.Keep();
            }
            return Path;
        }

        public JsonResult search(string _title, int _tag)
        {
            List l = new List();
            if (_title != "" && _tag == 3000)
            {
                l.Title = _title;
                return l.SearchByTitle().DataInJSON;
            }
            else if (_title == "" && _tag != 3000)
            {
                l.Tag = _tag;
                return l.SearchByTag().DataInJSON;
            }
            else if (_title != "" && _tag != 3000)
            {
                l.Title = _title;
                l.Tag = _tag;
                return l.SearchByTItleTag().DataInJSON;
            }
            return new JsonResult();
        }

        public string DeleteModerator(int _ID)
        {
            Moderator M = new Moderator();
            M.ID = _ID;
            return M.RemoveAdmin().Message.ShowMsg();
        }

        public string UpdateAdmin(int _ID, string Email, string Password, bool CreateAdmin, bool SethotList, bool CreateList, bool ApproveList)
        {
            Moderator M = new Moderator();
            M.ID = _ID;
            M.Email = Email;
            M.ApproveLists =ApproveList ;
            M.CreateAdmin = CreateAdmin ;
            M.CreateList = CreateList ;
            M.SetHotLists = SethotList ;
            M.password = Password;
            return M.EditAdmin().Message.ShowMsg();
        }

        [HttpPost]
        public JsonResult GetModeratorData(int _ID)
        {
            Moderator M = new Moderator();
            M.ID = _ID;
            return M.GetByID().DataInJSON;
        }


        public string Confirm(int _id)
        {
            List l = new List();
            l.ID = _id;
            var res = l.ConfirmList();
            return res.Message.ShowMsg();
        }

        public string Remove(int _id)
        {
            List l = new List();
            l.ID = _id;
            return l.RemoveList().Message.ShowMsg();
        }

        public string SetHot(int _id)
        {
            List l = new List();
            l.ID = _id;
            return l.SetHotList().Message.ShowMsg();
        }

        public string NotHot(int _id)
        {
            List l = new List();
            l.ID = _id;
            return l.NotHot().Message.ShowMsg();
        }

        public JsonResult GetPermission()
        { 
            Moderator M = new Moderator();
            M = Session["moderator"] as Moderator;
            return GetModeratorData(M.ID);
        }
    }
}
