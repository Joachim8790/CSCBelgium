﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSCBelgium.DAO.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CSCbelgiumDatabaseEntities : DbContext
    {
        public CSCbelgiumDatabaseEntities()
            : base("name=CSCbelgiumDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBrands> tblBrands { get; set; }
        public virtual DbSet<tblColors> tblColors { get; set; }
        public virtual DbSet<tblImages> tblImages { get; set; }
        public virtual DbSet<tblPosts> tblPosts { get; set; }
        public virtual DbSet<tblCars> tblCars { get; set; }
        public virtual DbSet<tblSlides> tblSlides { get; set; }
        public virtual DbSet<tblRimImages> tblRimImages { get; set; }
        public virtual DbSet<tblRims> tblRims { get; set; }
        public virtual DbSet<tblRimBrands> tblRimBrands { get; set; }
    }
}
