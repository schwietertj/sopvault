using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SopVault.Data;
using SopVault.Models;

namespace SopVault.Repository
{
    public class DocumentVersionRepository : GenericRepository<DocumentVersion>, IDocumentVersionRepository
    {
        private readonly ApplicationDbContext _ctx;

        public DocumentVersionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ctx = dbContext;
        }

        public override async Task<DocumentVersion> Upsert(DocumentVersion entity)
        {
            if (entity.Id > 0)
            {
                var nextVersion = 1;
                if (await _ctx.DocumentVersions.AnyAsync(x => x.DocumentId == entity.DocumentId))
                {
                    nextVersion = (await _ctx.DocumentVersions.Where(x => x.DocumentId == entity.DocumentId).MaxAsync(x => x.Version)) + 1;
                }

                entity.Version = nextVersion;

                await _ctx.Set<DocumentVersion>().AddAsync(entity);
            }
            else
            {
                _ctx.Set<DocumentVersion>().Update(entity);
            }

            await _ctx.SaveChangesAsync();

            return entity;
        }
    }
}
