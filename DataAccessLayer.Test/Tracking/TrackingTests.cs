using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit;

namespace DataAccessLayer.Test.Tracking
{
    public class TrackingTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public TrackingTests()
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
            Product product = _context.Product.First();

            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();

            //Tracking Is on
            Assert.Single(entries);

            var name = product.Name;
            product.Name = "foo";

            Assert.Equal(name,
                entries[0].OriginalValues[nameof(product.Name)].ToString()
                );
            Assert.Equal(product.Name,
                entries[0].CurrentValues[nameof(product.Name)].ToString()
                );

            Assert.NotEqual(EntityState.Modified, entries[0].State);
        }

        [Fact]
        public void Test2()
        {
            Product product = _context.Product.AsNoTracking().First();

            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();

            //Tracking is Off
            Assert.Empty(entries);
        }

        [Fact]
        public void Test3()
        {
            _context.ChangeTracker.QueryTrackingBehavior =
                QueryTrackingBehavior.NoTracking;

            Product product = _context.Product.First();

            List<EntityEntry<Product>> entries =
                _context.ChangeTracker.Entries<Product>().ToList();

            //Tracking is Off
            Assert.Empty(entries);
        }
    }
}