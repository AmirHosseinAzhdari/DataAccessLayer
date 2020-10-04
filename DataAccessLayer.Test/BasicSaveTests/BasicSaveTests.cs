using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.BasicSaveTests
{
   public class BasicSaveTests:IDisposable
    {
        private readonly AdventureWorksContext _context;
        public BasicSaveTests()
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
           // _context.Product.Add(product);
           Assert.Equal(EntityState.Detached,_context.Entry(product).State);
           Assert.Empty(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void Test2()
        {
            var productUnChanged = _context.Product.First();
            // If we change the query == changed
            Assert.Equal(EntityState.Unchanged , _context.Entry(productUnChanged).State);
            Assert.Single(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void Test3()
        {
            var product1 = TestHelpers.CreateProduct("1");
            _context.Add(product1);
            Assert.Equal(EntityState.Added,_context.Entry(product1).State);
            Assert.Single(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void Test4()
        {
            var productToRemove = _context.Product.OrderBy(p => p.ProductId)
                .First();
            _context.Remove(productToRemove);

            Assert.Equal(EntityState.Deleted,_context.Entry(productToRemove).State);
            Assert.Single(_context.ChangeTracker.Entries());
        }

        [Fact]
        public void Test5()
        {
            var productToUpdate = _context.Product.OrderBy(p => p.ProductId)
                .First();
            productToUpdate.Name += "Test";
            _context.Update(productToUpdate);
            Assert.Equal(EntityState.Modified,_context.Entry(productToUpdate).State);
        }

        [Fact]
        public void Test6()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var productToUpdate = _context.Product.OrderBy(p => p.ProductId)
                    .First();
                productToUpdate.Name += "Test";
                Assert.Equal(EntityState.Modified, _context.Entry(productToUpdate).State);
                _context.SaveChanges();
                Assert.Equal(EntityState.Unchanged, _context.Entry(productToUpdate).State);
                transaction.Rollback();
            }
        }

        [Fact]
        public void Test7()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var productToUpdate = _context.Product.OrderBy(p => p.ProductId)
                    .First();
                productToUpdate.Name += "Test";

                Assert.Equal(EntityState.Modified, _context.Entry(productToUpdate).State);

                _context.SaveChanges(false);
                _context.ChangeTracker.AcceptAllChanges();

                Assert.Equal(EntityState.Unchanged, _context.Entry(productToUpdate).State);
                transaction.Rollback();
            }
        }
    }
}
