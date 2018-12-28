using System.Linq;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        IQueryable<Document> GetAllByDepartmentId(long id);
    }
}
