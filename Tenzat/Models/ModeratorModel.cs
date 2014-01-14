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
            db.Moderators.Add(this);
            db.SaveChanges();
            var lastModerator = db.Moderators.OrderByDescending(p => p.ID).FirstOrDefault();
            return new Returner
            {
                Data = lastModerator,
                Message = Msgs.Moderator_Created_Successfully
            };
        }

        public Returner ModeratorLogin(string _Email,string _password)
        {
            var login = db.Moderators.Any(p => p.Email == _Email && p.password == _password);
            if (login == true)
            {
                var Moderator = (from M in db.Moderators
                                 where M.Email == _Email && M.password == _password
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
                       select m).ToList();
            return all;
        }
    }
}