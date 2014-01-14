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
    public class Videos {
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
             var res = mod.ModeratorLogin(_mail, _pass);
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

        public ActionResult ControlPanel()
        {
            return View();
        }

        public ActionResult ModifyAdmin()
        {
            Moderator M = new Moderator();
            List<Moderator> LOM = new List<Moderator>();
            LOM = M.allmod();
            ViewBag.Moderators = LOM;
            return View();
         }

        public ActionResult CreateList()
        {
            string[] Tags = Enum.GetNames(typeof(Tags));
            ViewBag.Tags = Tags;
            return View();
        }

        public string AddList(string ListTitle, string listtype, string ImageURL, string VideoURL, int Tag, string[] ListItems,string VideosUrls)
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
            L.CreatedBy = (Session["moderator"]as Moderator).ID;
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

            return View();
        }

        public string CreatModerator(string _email, string _paass,Boolean _manage,Boolean _create,Boolean _hot,Boolean _set)
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


        public ActionResult SetHotList()
        {
            return View();
        }
        public ActionResult SearchList()
        {
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
                if (LOLI.Any(p=>p.Order == orderid))
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


        //public Returner Search(string _title, string _tag)
        //{
       
        //    List search = new List();
        //    if (search.Tag = _tag)
        //    {
        //        var res = search.SearchByTitle(_title);
        //        return res;
        //    }
        //}

        public List<List> serchtitle(string _title)
        {
            List l = new List();
            var res = ((l.SearchByTitle(_title)).Data) as List<List>;
            ViewBag.searchrestitle = res;
            return res;
        }
        
        public List<List> serchtag(int _tag)
        {
            List l = new List(); 
            var res = ((l.SearchByTag(_tag)).Data) as List<List>;
            ViewBag.searchrestag = res;
            return res;
        }

        public List<List> SearchTagTitle(string _title, int _tag)
        {
            List l = new List();
            var res = ((l.SearchByTItleTag(_title,_tag)).Data) as List<List>;
            ViewBag.searchrestagtitle = res; 
            return res;
        }

        
    }
}
