using System;
using System.Linq;
using DataAccessLayer.Models.Context;
using Xunit;

namespace DataAccessLayer.Test.ClientServerEval
{
    public class ClientServerEvalTests : IDisposable
    {
        private readonly AdventureWorksContext _context;
        public ClientServerEvalTests()
        {
            _context = new AdventureWorksContext();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        [Fact]
        //Because we Have Client Warning This Method Is Returns exceptions
        public void Test1()
        {
            Assert.Throws<InvalidOperationException>(() =>
                _context.Product.LastOrDefault());

            // _context.Product.OrderByDescending(p=>p.ProductId).FirstOrDefault();
        }
    }
}