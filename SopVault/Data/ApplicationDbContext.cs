using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SopVault.Models;
using SopVault.Services.AppContext;

namespace SopVault.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentVersion> DocumentVersions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Set default active value
            builder.Entity<Department>().Property(x => x.Active).HasDefaultValue(true);
            builder.Entity<Document>().Property(x => x.Active).HasDefaultValue(true);
            builder.Entity<DocumentVersion>().Property(x => x.Active).HasDefaultValue(true);

            // Department
            builder.Entity<Department>().HasIndex(x => new { x.Abbreviation }).IsUnique();

            // Document
            builder.Entity<Document>().HasIndex(x => new {x.DepartmentId, x.DocumentNumber}).IsUnique();

            // Document Version
            builder.Entity<DocumentVersion>().HasIndex(x => new { x.DocumentId, x.Version}).IsUnique();

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).Created = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreatedBy = AppContextHelper.Current.User.Identity.Name;
                }

                ((BaseEntity)entity.Entity).Modified = DateTime.UtcNow;
                ((BaseEntity) entity.Entity).ModifiedBy = AppContextHelper.Current.User.Identity.Name;
            }
        }

    }
}
