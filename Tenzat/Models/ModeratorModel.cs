using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tenzat.Models
{
    public partial class Moderator
    {
        #region Context
        TenzatDBEntities db = new TenzatDBEntities();
        #endregion

        public Returner CreateModerator()
        {
            var newMod = db.Moderators.Any(p => p.Email == this.Email);
            if (newMod == false)
            {
                db.Moderators.Add(this);
                db.SaveChanges();
                var lastModerator = db.Moderators.OrderByDescending(p => p.ID).FirstOrDefault();
                return new Returner
                {
                    Data = lastModerator,
                    Message = Msgs.Moderator_Created_Successfully
                };
            }
            return new Returner
            {
                Message = Msgs.Email_Already_Used
            };
        }

        public Returner ModeratorLogin()
        {
            var login = db.Moderators.Any(p => p.Email == this.Email && p.password == this.password);
            if (login == true)
            {
                var Moderator = (from M in db.Moderators
                                 where M.Email == this.Email && M.password == this.password
                                 select M).SingleOrDefault();
                return new Returner
                {
                    Data = Moderator,
                    Message = Msgs.Successful_Login
                };
            }
            return new Returner
            {
                Message = Msgs.Username_Or_Password_Is_Incorrect
            };
        }

        public List<Moderator> allmod()
        {
            var all = (from m in db.Moderators
                       where m.Status!=2
                       select m).ToList();
            return all;
        }

        public Returner RemoveAdmin()
        {
            var removeAdmin = db.Moderators.Where(p => p.ID == this.ID).ToList().SingleOrDefault();
           // db.Moderators.Remove(removeAdmin);
            removeAdmin.Status = (int)ListStatus.Removed;
            db.SaveChanges();
            return new Returner
            {
                Data = removeAdmin,
                Message = Msgs.Admin_Deleted_Successfully
            };
        }

        public Returner EditAdmin()
        {
            var Admin = db.Moderators.Where(p=> p.ID == this.ID).SingleOrDefault();
            Admin.ApproveLists = this.ApproveLists;
            Admin.CreateAdmin = this.CreateAdmin;
            Admin.CreateList = this.CreateList;
            Admin.Email = this.Email;
            Admin.password = this.password;
            Admin.SetHotLists = this.SetHotLists;
            db.SaveChanges();
            return new Returner
            {
                Message = Msgs.Admin_Updated_Successfully
            };
        }

        public Returner GetByID()
        {
            var Admin = db.Moderators.Where(p => p.ID == this.ID).ToList();
            var JSONAdmin = (from A in Admin
                             select new
                             {
                                 A.ApproveLists,
                                 A.CreateAdmin,
                                 A.CreateList,
                                 A.Email,
                                 A.ID,
                                 A.password,
                                 A.SetHotLists,
                                 A.Status
                             }).Cast<object>().ToList().SingleOrDefault();
            return new Returner 
            { 
                Data = Admin.SingleOrDefault(),
                DataInJSON = JSONAdmin.ToJSON()
            };
        }
    }
}