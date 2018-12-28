using Microsoft.EntityFrameworkCore.Design;
using System;
using Microsoft.EntityFrameworkCore;
using SopVaultDataModels.Data;

namespace SopVaultDataModels
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            try
            {
                DotNetEnv.Env.Load();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            if (string.IsNullOrWhiteSpace(DotNetEnv.Env.GetString("connectionstring")))
                throw new NullReferenceException("connectionstring environment variable is null.");

            optionsBuilder.UseSqlServer(DotNetEnv.Env.GetString("connectionstring") ?? "");
            return new ApplicationDbContext(optionsBuilder.Options);

        }
    }
}
