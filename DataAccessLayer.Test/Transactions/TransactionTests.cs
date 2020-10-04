using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Xunit;

namespace DataAccessLayer.Test.Transactions
{
    public class TransactionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public TransactionTests()
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
            var count = _context.Product.Count();

            var product1 = TestHelpers.CreateProduct("1");
            _context.Product.Add(product1);

            //Not Fill Required Properties
            var product2 = new Product() { ProductId = 3 };
            _context.Product.Add(product2);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Assert.Equal(count, _context.Product.Count());
        }

        [Fact]
        public void Test2()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var count = _context.Product.Count();

                var product1 = TestHelpers.CreateProduct("1");
                _context.Product.Add(product1);

                var product2 = TestHelpers.CreateProduct("2");
                _context.Product.Add(product2);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                Assert.Equal(count + 2, _context.Product.Count());
                transaction.Rollback();
                Assert.Equal(count, _context.Product.Count());
            }
        }
    }
}
