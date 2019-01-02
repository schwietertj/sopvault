using System.Linq;
using System.Threading.Tasks;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        IQueryable<Document> GetAllByDepartmentId(long id);
        Task<bool> DocumentNumberExists(string documentNumber);
    }
}
