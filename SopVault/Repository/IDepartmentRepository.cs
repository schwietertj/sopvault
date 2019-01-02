using System.Threading.Tasks;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<Department> GetDetail(long id);
    }
}
