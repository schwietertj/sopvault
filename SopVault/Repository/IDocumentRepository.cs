using System.Linq;
using SopVault.Models;

namespace SopVault.Repository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        IQueryable<Document> GetAllByDepartmentId(long id);
    }
}
