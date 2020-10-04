using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.RelatedData
{
    public class RelatedDataTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public RelatedDataTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        //LazyLoad Not Supported From EF Core
        public void Test1()
        {
            List<Product> list = _context.Product
                .Where(p => p.ProductModelId == 7).ToList();
            Assert.Null(list[0].ProductModel);
        }

        [Fact]
        //We Use eager 
        public void Test2()
        {
            List<Product> list = _context.Product
                .Where(p => p.ProductModelId == 7)
                .Include(p => p.ProductModel)
                .ToList();
            Assert.NotNull(list[0].ProductModel);
        }

        [Fact]
        //ExplicitLoad
        public void Test3()
        {
            Product product = _context.Product
                .FirstOrDefault(p => p.ProductModelId == 7);
            _context.Entry(product).Reference(p => p.ProductModel).Load();
            Assert.NotNull(product.ProductModel);
            //second Relation
            _context.Entry(product.ProductModel)
                .Collection(pm => pm.ProductModelIllustration).Load();
            Assert.NotNull(product.ProductModel.ProductModelIllustration);
        }
    }
}