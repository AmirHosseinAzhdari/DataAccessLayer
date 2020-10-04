using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models.Entities;

namespace DataAccessLayer.Test
{
   public static class TestHelpers
    {
        public static Product CreateProduct(string itemQualifier)
        {
            return new Product()
            {
                ListPrice = 12M,
                Name = $"Test Product {itemQualifier}",
                SellStartDate = new DateTime(2018,10,07),
                ProductNumber = $"11234-{itemQualifier}",
                SafetyStockLevel = 500,
                ReorderPoint = 375
            };
        }
    }
}
