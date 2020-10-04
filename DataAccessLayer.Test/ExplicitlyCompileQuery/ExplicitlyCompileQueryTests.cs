using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.ExplicitlyCompileQuery
{
    public class ExplicitlyCompileQueryTests : IDisposable
    {
        private readonly AdventureWorksContext _context;

        public ExplicitlyCompileQueryTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public static Func<AdventureWorksContext, decimal, bool, IEnumerable<Product>>
            GetProductByListPriceAndMakeFlag =
                EF.CompileQuery((AdventureWorksContext context, decimal listPrice,
                    bool makeFlag) =>
                context.Product.Where(p => p.ListPrice >= listPrice && (p.MakeFlag ?? false) == makeFlag)
                );

        [Fact]
        public void Test1()
        {
            var productList = GetProductByListPriceAndMakeFlag(_context, 0M, true).ToList();
        }
    }
}