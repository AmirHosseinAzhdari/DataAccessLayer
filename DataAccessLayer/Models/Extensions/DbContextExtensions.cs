﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Models.Extensions
{
    public static class DbContextExtensions
    {
        //public static string GetSqlServerTableName<TEntity>(this DbContext context)
        //    where TEntity : class, new()
        //{
        //    var metaData = context.Model
        //          .FindEntityType(typeof(TEntity).FullName).SqlServer();
        //    return $"{metaData.Schema}.{metaData.TableName}";
        //}
    }
}