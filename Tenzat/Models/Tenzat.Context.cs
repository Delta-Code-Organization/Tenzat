﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tenzat.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TenzatDBEntities : DbContext
    {
        public TenzatDBEntities()
            : base("name=TenzatDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<List> Lists { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}