using Microsoft.EntityFrameworkCore;
using SopVaultDataModels.Data;
using System;
using System.Linq;

namespace SopVault.ExtensionMethods
{
    public static class ContextExtensions
    {
        public static IQueryable<T> DynamicInclude<T>(this ApplicationDbContext dbContext, string relatedObjects) where T : class
        {
            var query = dbContext.Set<T>().AsQueryable();

            foreach (var relatedObject in relatedObjects.Split(",", StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(relatedObject);

            return query;

        }
    }
}
