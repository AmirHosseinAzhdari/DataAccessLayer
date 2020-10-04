using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.RawSqlWithLinq
{
    public class RawSqlWithLinqTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public RawSqlWithLinqTests()
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
            var sqlQuery = "Select * From Production.Product";
            var products = _context.Product
                .FromSqlRaw(sqlQuery).IgnoreQueryFilters()
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                .ToList();

            Assert.NotNull( products[0].ProductModel);
        }
    }
}