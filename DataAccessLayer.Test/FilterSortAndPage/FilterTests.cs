using DataAccessLayer.Models.Context;
using System;
using System.Linq;
using DataAccessLayer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DataAccessLayer.Test.FilterSortAndPage
{
    public class FilterTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public FilterTests()
        {
            _context = new AdventureWorksContext();
        }

        [Fact]
        public void ShouldFindWithPrimaryKey()
        {
            var product = _context.Product.Find(3);
            Assert.Equal("BB Ball Bearing", product.Name, ignoreCase: true);
        }

        [Fact]
        public void FilteringResultWithComplexKey()
        {
            ProductVendor pv = _context.ProductVendor.Find(2, 1688);

            Assert.Equal(5, pv.MaxOrderQty);
            Assert.Equal(3, pv.OnOrderQty);
        }

        [Fact]
        public void ShouldThrowWhenFirstFails()
        {
            Assert.Throws<InvalidOperationException>(() =>
                _context.Product.First(p => p.ProductId == -1)
                );
        }

        /// <summary>
        /// Must failed 
        /// </summary>
        [Fact]
        public void ShouldGetTheLastRecord()
        {
            // Gets All Records And Select The Last One
            // Last Product Name Is True
            Assert.Throws<InvalidOperationException>(() =>
                _context.Product.LastOrDefault(p => p.MakeFlag ?? true));
        }

        /// <summary>
        /// Order OrderByDescending ThenBy ThenByDescending
        /// </summary>
        [Fact]
        public void ShouldSortData()
        {
            IOrderedQueryable<Product> query = _context.Product
                .Where(p => p.ListPrice != 0)
                .OrderBy(p => p.DaysToManufacture)
                .ThenByDescending(p => p.ProductId);
            var productList = query.ToList();

            Assert.NotEmpty(productList);
        }

        /// <summary>
        /// Paging
        /// </summary>
        [Fact]
        public void ShouldSkipFirst25AndTake30Records()
        {
            //406 - 25 = 381 (Take)=> 30
            var productList = _context.Product
                .Where(p => p.SellEndDate == null)
                .Skip(25)
                .OrderBy(p => p.Name)
                .Take(30).ToList();

            Assert.Equal(30, productList.Count);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
