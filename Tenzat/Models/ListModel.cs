using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tenzat.Models
{
    public partial class List
    {
        #region Context
        TenzatDBEntities db = new TenzatDBEntities();
        #endregion

        public Returner GetListByDAte()
        {
            var getListByDate = db.Lists.OrderByDescending(p => p.ListDate).ToList();

            var CustomList = (from L in getListByDate
                              select new
                              {
                                  L.CreatedBy,
                                  L.Rank,
                                  L.Status,
                                  L.Tag,
                                  L.Title,
                                  L.image,
                                  L.Views,
                                  L.FbShares,
                                  L.TwitterShares,
                                  L.Hot,
                                  L.ListType,
                                  ListDate,
                                  L.ID,
                                  Moderator = new {
                                      L.Moderator.CreateAdmin,
                                      L.Moderator.CreateList,
                                      L.Moderator.Email,
                                      L.Moderator.ID,
                                      L.Moderator.SetHotLists,
                                      L.Moderator.Status
                                  },
                                  ListItems = new List<object>((from LI in L.ListItems
                                                           select new
                                                           {
                                                               LI.Order,
                                                               LI.ItemType,
                                                               LI.ID,
                                                               LI.ListID,
                                                               LI.MoreText,
                                                               LI.Title,
                                                               LI.Description,
                                                               LI.Drawable
                                                           }).Cast<object>().ToList()).Cast<object>().ToList()
                              }).ToList();
            return new Returner
            {
                Data = getListByDate,
                DataInJSON = CustomList.ToJSON()
                
            };
        }

        public Returner GetListByRank()
        {
            var getListByRank = db.Lists.OrderByDescending(p => p.Rank).ToList().Take(6);
            var CustomList = (from L in getListByRank
                              select new
                              {
                                  L.CreatedBy,
                                  L.Rank,
                                  L.Status,
                                  Tag = Enum.GetName(typeof(Tags), L.Tag),
                                  L.Title,
                                  L.image,
                                  L.Views,
                                  L.FbShares,
                                  L.TwitterShares,
                                  L.Hot,
                                  L.ListType,
                                  ListDate,
                                  L.ID,
                                  Moderator = new {
                                      L.Moderator.CreateAdmin,
                                      L.Moderator.CreateList,
                                      L.Moderator.Email,
                                      L.Moderator.ID,
                                      L.Moderator.SetHotLists,
                                      L.Moderator.Status
                                  },
                                  ListItems = new List<object>((from LI in L.ListItems
                                                           select new
                                                           {
                                                               LI.Order,
                                                               LI.ItemType,
                                                               LI.ID,
                                                               LI.ListID,
                                                               LI.MoreText,
                                                               LI.Title,
                                                               LI.Description,
                                                               LI.Drawable
                                                           }).Cast<object>().ToList()).Cast<object>().ToList()
                              }).ToList();
            return new Returner
            {
                Data = getListByRank,
                DataInJSON = CustomList.ToJSON()
                
            };
        }

        public Returner GetListByRankAjax(List<int> IDS)
        {
            var getListByRank = db.Lists.OrderByDescending(p => p.Rank).Where(p => !IDS.Contains(p.ID)).Take(6).ToList();
            var CustomList = (from L in getListByRank
                              select new
                              {
                                  L.CreatedBy,
                                  L.Rank,
                                  L.Status,
                                  Tag = Enum.GetName(typeof(Tags), L.Tag),
                                  L.Title,
                                  L.image,
                                  L.Views,
                                  L.FbShares,
                                  L.TwitterShares,
                                  L.Hot,
                                  L.ListType,
                                  ListDate,
                                  L.ID,
                                  Moderator = new
                                  {
                                      L.Moderator.CreateAdmin,
                                      L.Moderator.CreateList,
                                      L.Moderator.Email,
                                      L.Moderator.ID,
                                      L.Moderator.SetHotLists,
                                      L.Moderator.Status
                                  },
                                  ListItems = new List<object>((from LI in L.ListItems
                                                                select new
                                                                {
                                                                    LI.Order,
                                                                    LI.ItemType,
                                                                    LI.ID,
                                                                    LI.ListID,
                                                                    LI.MoreText,
                                                                    LI.Title,
                                                                    LI.Description,
                                                                    LI.Drawable
                                                                }).Cast<object>().ToList()).Cast<object>().ToList()
                              }).ToList();
            return new Returner
            {
                Data = getListByRank,
                DataInJSON = CustomList.ToJSON()

            };
        }

        public Returner CreateList()
        {
            var title = db.Lists.Any(p => p.Title == this.Title);
           
            if (title == true)
            {
                return new Returner
                {
                    Message = Msgs.List_name_dublicated
                };
            }
            db.Lists.Add(this);
            db.SaveChanges();
            var lastList = db.Lists.OrderByDescending(p => p.ID).FirstOrDefault();
            return new Returner
            {
                Data = lastList,
                Message = Msgs.List_Created_Successfully
            };
        }

        public Returner SetHotList(int _ListID)
        {
            var hotList =db.Lists.Where(p => p.ID == _ListID).ToList().SingleOrDefault();
            hotList.Hot = true;
            db.SaveChanges();
            return new Returner 
            {
                Data = hotList,
                Message= Msgs.Set_Hot_List_Done_Successfully
            };
            
        }

        //public Returner GetList()
        //{
        //    var myList = (from L in db.Lists
        //                      orderby L.ID descending
        //                      select L).ToList();
        //    var customList=(from cl in myList
        //                    select new
        //                    {
        //                        cl.CreatedBy,
        //                        cl.Rank,
        //                        cl.Status,
        //                        cl.Tag,
        //                        cl.Title,
        //                        cl.image,
        //                        cl.Views,
        //                        cl.FbShares,
        //                        cl.TwitterShares,
        //                        cl.Hot,
        //                        cl.ListType,
        //                        cl.ListDate,
        //                        cl.ID,
        //                        Moderator = new
        //                        {
        //                            cl.Moderator.CreateAdmin,
        //                            cl.Moderator.CreateList,
        //                            cl.Moderator.Email,
        //                            cl.Moderator.ID,
        //                            cl.Moderator.SetHotLists,
        //                            cl.Moderator.Status
        //                        },
        //                        ListItems = new List<object>((from LI in cl.ListItems
        //                                                      select new
        //                                                      {
        //                                                          LI.Order,
        //                                                          LI.ItemType,
        //                                                          LI.ID,
        //                                                          LI.ListID,
        //                                                          LI.MoreText,
        //                                                          LI.Title,
        //                                                          LI.Description,
        //                                                          LI.Drawable
        //                                                      }).Cast<object>().ToList()).Cast<object>().ToList()
        //                    }).ToList();
        //    return new Returner
        //    {
        //        Data = myList,
        //        DataInJSON =customList.ToJSON()
        //    };
        //}

        public Returner GetListByID(int _ID)
        {
            var myList = db.Lists.Where(p => p.ID ==_ID).ToList();
            var myCustomList = (from cl in myList
                              select new
                            {
                                cl.CreatedBy,
                                cl.Rank,
                                cl.Status,
                                Tag = Enum.GetName(typeof(Tags), cl.Tag),
                                cl.Title,
                                cl.image,
                                cl.Views,
                                cl.FbShares,
                                cl.TwitterShares,
                                cl.Hot,
                                cl.ListType,
                                cl.ListDate,
                                cl.ID,
                                Moderator = new
                                {
                                    cl.Moderator.CreateAdmin,
                                    cl.Moderator.CreateList,
                                    cl.Moderator.Email,
                                    cl.Moderator.ID,
                                    cl.Moderator.SetHotLists,
                                    cl.Moderator.Status
                                },
                                ListItems = new List<object>((from LI in cl.ListItems
                                                              select new
                                                              {
                                                                  LI.Order,
                                                                  LI.ItemType,
                                                                  LI.ID,
                                                                  LI.ListID,
                                                                  LI.MoreText,
                                                                  LI.Title,
                                                                  LI.Description,
                                                                  LI.Drawable
                                                              }).Cast<object>().ToList()).Cast<object>().ToList()
                            }).ToList().FirstOrDefault();
            return new Returner
            {
                Data = myList.FirstOrDefault(),
                DataInJSON = myCustomList.ToJSON()
            }; 
        }

        public Returner ConfirmList(int _ID)
        {
            var confirm = db.Lists.Where(p => p.ID == _ID).SingleOrDefault();
            confirm.Status=(int)ListStatus.Confirmed;
            db.SaveChanges();
            return new Returner
            {
                Data = confirm,
                Message = Msgs.List_Confirmed
            };
        }

        public Returner RemoveList(int _ID)
        {
            var remove = db.Lists.Where(p => p.ID == _ID).ToList().SingleOrDefault();
            remove.Status = (int)ListStatus.Removed;
            db.SaveChanges();
            return new Returner
            {
                Data = remove,
                Message = Msgs.List_Removed
            };
        }

        public Returner SetListItem(ListItem Li)
        {
            Li.ListID = this.ID;
            db.ListItems.Add(Li);
            db.SaveChanges();
            var lastItem = db.ListItems.OrderByDescending(p => p.ID).FirstOrDefault();
            return new Returner
            {
                Data=lastItem,
                Message = Msgs.List_Item_Created_Successfully
            };
        }

        public Returner SearchByTag(int _Tag)
        {
            var tag = (from t in db.Lists
                       where t.Tag == _Tag
                       select t).ToList();
            var tagList=(from tl in tag
                        select new {
                            tl.CreatedBy,
                            tl.Rank,
                            tl.Status,
                            tl.Tag,
                            tl.Title,
                            tl.image,
                            tl.Views,
                            tl.FbShares,
                            tl.TwitterShares,
                            tl.Hot,
                            tl.ListType,
                            tl.ListDate,
                            tl.ID,
                            Moderator = new
                            {
                                tl.Moderator.CreateAdmin,
                                tl.Moderator.CreateList,
                                tl.Moderator.Email,
                                tl.Moderator.ID,
                                tl.Moderator.SetHotLists,
                                tl.Moderator.Status
                            },
                                ListItems=new List<object>((from LI in tl.ListItems
                                                              select new
                                                              {
                                                                  LI.Order,
                                                                  LI.ItemType,
                                                                  LI.ID,
                                                                  LI.ListID,
                                                                  LI.MoreText,
                                                                  LI.Title,
                                                                  LI.Description,
                                                                  LI.Drawable
                                                              }).Cast<object>().ToList()).Cast<object>().ToList()
                        }).ToList();
            return new Returner {
                Data=tag,
                DataInJSON=tagList.ToJSON()
            };
        }
        public Returner SearchByTitle(string _Title)
        {
            var searchByTitle = db.Lists.Where(p => p.Title.Contains(_Title)).ToList();
            var titleList=(from tl in searchByTitle
                           select new
                           {
                               tl.CreatedBy,
                               tl.Rank,
                               tl.Status,
                               tl.Tag,
                               tl.Title,
                               tl.image,
                               tl.Views,
                               tl.FbShares,
                               tl.TwitterShares,
                               tl.Hot,
                               tl.ListType,
                               tl.ListDate,
                               tl.ID,
                               Moderator = new
                               {
                                   tl.Moderator.CreateAdmin,
                                   tl.Moderator.CreateList,
                                   tl.Moderator.Email,
                                   tl.Moderator.ID,
                                   tl.Moderator.SetHotLists,
                                   tl.Moderator.Status
                               },
                               ListItems = new List<object>((from LI in tl.ListItems
                                                             select new
                                                             {
                                                                 LI.Order,
                                                                 LI.ItemType,
                                                                 LI.ID,
                                                                 LI.ListID,
                                                                 LI.MoreText,
                                                                 LI.Title,
                                                                 LI.Description,
                                                                 LI.Drawable
                                                             }).Cast<object>().ToList()).Cast<object>().ToList()
                           }).ToList();
            return new Returner
            {
                Data=searchByTitle,
                DataInJSON=titleList.ToJSON()
            };         
        }

        public Returner SearchByTItleTag(string _Title, int _Tag)
        {
            var Search = db.Lists.Where(p => p.Title == _Title && p.Tag == _Tag).ToList();
            var List = (from tl in Search
                           select new
                           {
                               tl.CreatedBy,
                               tl.Rank,
                               tl.Status,
                               tl.Tag,
                               tl.Title,
                               tl.image,
                               tl.Views,
                               tl.FbShares,
                               tl.TwitterShares,
                               tl.Hot,
                               tl.ListType,
                               tl.ListDate,
                               tl.ID,
                               Moderator = new
                               {
                                   tl.Moderator.CreateAdmin,
                                   tl.Moderator.CreateList,
                                   tl.Moderator.Email,
                                   tl.Moderator.ID,
                                   tl.Moderator.SetHotLists,
                                   tl.Moderator.Status
                               },
                               ListItems = new List<object>((from LI in tl.ListItems
                                                             select new
                                                             {
                                                                 LI.Order,
                                                                 LI.ItemType,
                                                                 LI.ID,
                                                                 LI.ListID,
                                                                 LI.MoreText,
                                                                 LI.Title,
                                                                 LI.Description,
                                                                 LI.Drawable
                                                             }).Cast<object>().ToList()).Cast<object>().ToList()
                           }).ToList();
            return new Returner
            {
                Data = Search,
                DataInJSON = List.ToJSON()
            };
        }

        public Returner GetListItems(int _ID)
        {
            var getListItem = db.ListItems.Where(p => p.ListID ==_ID).ToList();
            var getListItemInjson = (from LI in getListItem
                                  select new
                                  {
                                   LI.Order,
                                   LI.ItemType,
                                   LI.ID,
                                   LI.ListID,
                                   LI.MoreText,
                                   LI.Title,
                                   LI.Description,
                                   LI.Drawable,
                                   List=new
                                   {
                             LI.List.CreatedBy,
                            LI.List.Rank,
                            LI.List.Status,
                            Tag = Enum.GetName(typeof(Tags), LI.List.Tag),
                            LI.List.Title,
                            LI.List.image,
                            LI.List.Views,
                            LI.List.FbShares,
                            LI.List.TwitterShares,
                            LI.List.Hot,
                            LI.List.ListType,
                            LI.List.ListDate,
                            LI.List.ID,
                            Moderator = new
                            {
                                LI.List.Moderator.CreateAdmin,
                                LI.List.Moderator.CreateList,
                                LI.List.Moderator.Email,
                                LI.List.Moderator.ID,
                                LI.List.Moderator.SetHotLists,
                                LI.List.Moderator.Status
                            }
                                   }
                                  }).ToList();
            return new Returner
            {
                Data = getListItem,
                DataInJSON = getListItemInjson.ToJSON()
            };         
        }

        public Returner GetHotList()
        {
            var Hot = (from L in db.Lists
                       where L.Hot == true
                       orderby L.Rank descending
                       select L).ToList();
            var HotJson = (from tl in Hot
                           select new
                           {
                               tl.CreatedBy,
                               tl.Rank,
                               tl.Status,
                               Tag = Enum.GetName(typeof(Tags), tl.Tag),
                               tl.Title,
                               tl.image,
                               tl.Views,
                               tl.FbShares,
                               tl.TwitterShares,
                               tl.Hot,
                               tl.ListType,
                               tl.ListDate,
                               tl.ID,
                               Moderator = new
                               {
                                   tl.Moderator.CreateAdmin,
                                   tl.Moderator.CreateList,
                                   tl.Moderator.Email,
                                   tl.Moderator.ID,
                                   tl.Moderator.SetHotLists,
                                   tl.Moderator.Status
                               },
                               ListItems = new List<object>((from LI in tl.ListItems
                                                             select new
                                                             {
                                                                 LI.Order,
                                                                 LI.ItemType,
                                                                 LI.ID,
                                                                 LI.ListID,
                                                                 LI.MoreText,
                                                                 LI.Title,
                                                                 LI.Description,
                                                                 LI.Drawable
                                                             }).Cast<object>().ToList()).Cast<object>().ToList()
                           }).ToList().FirstOrDefault();
            return new Returner
            {
                Data = Hot.FirstOrDefault(),
                DataInJSON = HotJson.ToJSON()
            };
        }

        public Returner GetRelatedLists(int _ID)
        {
            var related = db.Lists.OrderByDescending(p => p.Rank).Where(p => p.ID != _ID).Take(3).ToList();
            var relatedInJson=(from L in related
                               select new
                              {
                                  L.CreatedBy,
                                  L.Rank,
                                  L.Status,
                                  Tag = Enum.GetName(typeof(Tags), L.Tag),
                                  L.Title,
                                  L.image,
                                  L.Views,
                                  L.FbShares,
                                  L.TwitterShares,
                                  L.Hot,
                                  L.ListType,
                                  ListDate,
                                  L.ID,
                                  Moderator = new
                                  {
                                      L.Moderator.CreateAdmin,
                                      L.Moderator.CreateList,
                                      L.Moderator.Email,
                                      L.Moderator.ID,
                                      L.Moderator.SetHotLists,
                                      L.Moderator.Status
                                  },
                                  ListItems = new List<object>((from LI in L.ListItems
                                                                select new
                                                                {
                                                                    LI.Order,
                                                                    LI.ItemType,
                                                                    LI.ID,
                                                                    LI.ListID,
                                                                    LI.MoreText,
                                                                    LI.Title,
                                                                    LI.Description,
                                                                    LI.Drawable
                                                                }).Cast<object>().ToList()).Cast<object>().ToList()
                              }).ToList();
            return new Returner
            {
                Data = related,
                DataInJSON = relatedInJson.ToJSON()

            };
                              
        }

    }
}