using System;
using System.Linq;
using DataAccessLayer.Models.Context;
using Xunit;

namespace DataAccessLayer.Test.ScalarFunctionMap
{
    public class ScalarFunctionTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ScalarFunctionTests()
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
            //DbContext Runs in Server Side
            var productList = _context.Product
                .Where(p => AdventureWorksContext.GetStock(p.ProductId) > 4).ToList();

            Assert.Equal(191, productList.Count);
        }
    }
}