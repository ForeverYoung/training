﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrenairTraining.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OrenairTrainingEntities : DbContext
    {
        public OrenairTrainingEntities()
            : base("name=OrenairTrainingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<answer> answer { get; set; }
        public DbSet<container> container { get; set; }
        public DbSet<containertodepartment> containertodepartment { get; set; }
        public DbSet<containertype> containertype { get; set; }
        public DbSet<department> department { get; set; }
        public DbSet<ipaddress> ipaddress { get; set; }
        public DbSet<log> log { get; set; }
        public DbSet<material> material { get; set; }
        public DbSet<objectcode> objectcode { get; set; }
        public DbSet<question> question { get; set; }
        public DbSet<questiontype> questiontype { get; set; }
        public DbSet<role> role { get; set; }
        public DbSet<session> session { get; set; }
        public DbSet<sysdiagrams> sysdiagrams { get; set; }
        public DbSet<testconfig> testconfig { get; set; }
        public DbSet<testtouser> testtouser { get; set; }
        public DbSet<user> user { get; set; }
    }
}
