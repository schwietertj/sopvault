using System.Linq;
using System.Threading.Tasks;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(long id);

        Task Create(TEntity entity);

        Task Update(long id, TEntity entity);

        Task Delete(long id);

        Task<TEntity> Upsert(TEntity entity);
    }
}
