﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tabels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmplyeePageEntities : DbContext
    {
        public EmplyeePageEntities()
            : base("name=EmplyeePageEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<employee>().HasOptional(x => x.department1);
            modelBuilder.Entity<employee>().HasKey<int>(i => i.id);
            modelBuilder.Entity<department>().HasKey<int>(i => i.id);
        
            

        }
    
        public virtual DbSet<department> departments { get; set; }
        public virtual DbSet<employee> employees { get; set; }


    }
}
