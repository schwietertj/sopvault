using System.Linq;
using Microsoft.EntityFrameworkCore;
using SopVault.Data;
using SopVault.Models;

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
            return _ctx.Set<Document>().AsNoTracking().Where(x => x.DepartmentId == id);
        }
    }
}
