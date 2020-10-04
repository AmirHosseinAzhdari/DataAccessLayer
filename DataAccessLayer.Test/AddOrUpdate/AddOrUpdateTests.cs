using DataAccessLayer.EfStructures.Extensions;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace DataAccessLayer.Test.AddOrUpdate
{
    public class AddOrUpdateTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public AddOrUpdateTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void Test1()
        {
            var product = TestHelpers.CreateProduct("1");
            _context.Product.AddOrUpdate(product, p => p.Name);

            var newEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == product.ProductId);

            Assert.NotNull(newEntry);
            Assert.Equal(EntityState.Added, newEntry.State);
        }

        [Fact]
        public void Test2()
        {
            var product = TestHelpers.CreateProduct("15");
            //This name was Exist in database 
            product.Name = "BB Ball Bearing";
            _context.Product.AddOrUpdate(product, p => p.Name);

            var newEntry = _context.ChangeTracker.Entries<Product>()
                .FirstOrDefault(p => p.Entity.ProductId == product.ProductId);

            Assert.NotNull(newEntry);
            Assert.Equal(EntityState.Modified, newEntry.State);
        }
    }
}