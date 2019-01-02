using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SopVaultDataModels.Data;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _ctx;

        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ctx = dbContext;
        }

        public async Task<Department> GetDetail(long id)
        {
            return await _ctx.Departments.Include(x => x.Documents).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
