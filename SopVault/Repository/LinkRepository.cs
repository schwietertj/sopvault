using SopVaultDataModels.Data;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public class LinkRepository : GenericRepository<Link>, ILinkRepository
    {
        public LinkRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
