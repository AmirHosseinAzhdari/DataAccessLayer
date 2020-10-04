using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.Entities;
using Xunit;

namespace DataAccessLayer.Test.RelatedDataSave
{
    public class RelatedDataSaveTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public RelatedDataSaveTests()
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
            using (var transaction = _context.Database.BeginTransaction())
            {
                var catCount = _context.ProductCategory.Count();
                var subCatCount = _context.ProductSubcategory.Count();

                //Category and SubCategory have Relations
                var productCategory = new ProductCategory()
                {
                    Name = "New ProductCategory",
                    ProductSubcategory = new List<ProductSubcategory>()
                    {
                        new ProductSubcategory()
                        {
                            Name = "New SubCategory 1"
                        },
                        new ProductSubcategory()
                        {
                            Name = "New SubCategory 2"
                        },
                        new ProductSubcategory()
                        {
                            Name = "New SubCategory 3"
                        }
                    } 
                };
                _context.ProductCategory.Add(productCategory);
                _context.SaveChanges();
                Assert.Equal(catCount + 1, _context.ProductCategory.Count());
                Assert.Equal(subCatCount + 3, _context.ProductSubcategory.Count());

                _context.ProductSubcategory.RemoveRange(productCategory.ProductSubcategory);
                _context.ProductCategory.Remove(productCategory);
                _context.SaveChanges();

                transaction.Rollback();
            }
        }
    }
}