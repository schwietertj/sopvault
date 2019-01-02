using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SopVaultDataModels.Models;

namespace SopVaultDataModels.Data
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
            builder.Entity<Document>().HasIndex(x => new {x.DocumentNumber}).IsUnique();
            
            // Document Version
            builder.Entity<DocumentVersion>().HasIndex(x => new { x.DocumentId, x.Version}).IsUnique();

            base.OnModelCreating(builder);
        }
    }
}
