using System;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models.Context
{
    public partial class AdventureWorksContext
    {
        //1
        [DbFunction(name: "ufnGetStock", schema: "dbo")]
        public static int GetStock(int productId)
        {
            throw new NotImplementedException();
        }

        internal void AddModelCreatingConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasQueryFilter(p => p.SellEndDate == null);

            //2
            //modelBuilder.HasDbFunction(this.GetType().GetMethod("GetStock"),
            //    options =>
            //    {
            //        options.HasSchema("dbo");
            //        options.HasName("ufnGetStock");
            //    });
        }
    }
}
