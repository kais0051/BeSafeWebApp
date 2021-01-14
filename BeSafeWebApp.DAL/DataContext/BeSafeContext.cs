using Microsoft.EntityFrameworkCore;
using BeSafeWebApp.Contracts.Entities;
using Models = BeSafeWebApp.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BeSafeWebApp.DLL
{
    public class BeSafeContext : DbContext
    {
        //public string ConnectionString { get; set; }

        public BeSafeContext(DbContextOptions<BeSafeContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MasterItemsSet> MasterItemsSets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("User");
            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>(entity =>
            {
                entity
                    .HasMany(e => e.Children)
                    .WithOne(e => e.Parent)
                    .HasForeignKey(e => e.ParentCategoryId);
            });
            builder.Entity<MasterItemsSet>().ToTable("MasterItemsSet");
            

            //For GetProductListSp.
            //builder.Query<Models.ProductCM>();
            //Core 3.0:
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);

        //    //if (optionsBuilder.IsConfigured)
        //    //    return;

        //    //ConnectionString = Configuration.GetValue<string>("ConnectionStrings:StoreDbConnection");
        //    ConnectionString = "Server=(localdb)\\SQLLocal2016; Integrated Security = true; Initial Catalog = StoreCF8;";
        //    optionsBuilder.UseSqlServer(ConnectionString);
        //}
    }
}