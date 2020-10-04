using System.Linq;
using DataAccessLayer.Models.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.LikeTests
{
    public class LikeTests
    {
        private readonly AdventureWorksContext _context;
        public LikeTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        public void ShouldGetProductsUsingLike()
        {
            var products = _context.Product
                .Where(p => EF.Functions.Like(p.Name, "%Cara%")).ToList(); 

            Assert.NotNull(products);
        }
    }
}