using DataAccessLayer.Models.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.GlobalQueryFilters
{
    public class GlobalQueryFilterTests
    {
        private readonly AdventureWorksContext _context;
        public GlobalQueryFilterTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void Test()
        {
            var query = _context.Product.IgnoreQueryFilters().ToString();
            Assert.NotNull(query);
        }
    }
}