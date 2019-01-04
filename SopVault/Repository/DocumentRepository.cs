using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SopVaultDataModels.Data;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        private readonly ApplicationDbContext _ctx;

        public DocumentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ctx = dbContext;
        }

        public IQueryable<Document> GetAllByDepartmentId(long id)
        {
            return _ctx.Set<Document>().Include(x => x.DocumentVersions).ThenInclude(x => x.Links).AsNoTracking().Where(x => x.DepartmentId == id);
        }

        public async Task<bool> DocumentNumberExists(string documentNumber)
        {
            return await _ctx.Documents.AnyAsync(x => x.DocumentNumber.ToLower() == documentNumber.ToLower().Trim());
        }
    }
}
