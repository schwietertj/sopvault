using SopVaultDataModels.Data;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
