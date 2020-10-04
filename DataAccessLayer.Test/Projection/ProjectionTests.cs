using DataAccessLayer.Models.Context;
using DataAccessLayer.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataAccessLayer.Test.Projection
{
    public class ProjectionTests
    {
        private readonly AdventureWorksContext _context;
        public ProjectionTests()
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
            //All Of Them Is ServerSide
            //Better We Not Use var Instead Of var We use Dto Classes 
            List<ProjectionTestDto> newObjectList = _context.Product
                .Where(p => p.MakeFlag ?? true)
                .ToList()
                .Select(p => new ProjectionTestDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.ListPrice
                })
                .OrderBy(p => p.ProductId)
                .ToList();

            Assert.NotNull(newObjectList);
        }
    }
}