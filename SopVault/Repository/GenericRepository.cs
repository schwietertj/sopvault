using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SopVault.Services.AppContext;
using SopVaultDataModels.Data;
using SopVaultDataModels.Models;

namespace SopVault.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public virtual async Task<TEntity> GetById(long id)
        {
            return await _dbContext.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task Create(TEntity entity)
        {
            entity = SetTimestamps(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(long id, TEntity entity)
        {
            entity = SetTimestamps(entity);
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(long id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);

            if (entity != null)
            {
                entity.Active = false;
                await Update(id, entity);
            }
        }

        public virtual async Task<TEntity> Upsert(TEntity entity)
        {
            entity = SetTimestamps(entity);
            if (entity.Id > 0)
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
            }
            else
            {
                _dbContext.Set<TEntity>().Update(entity);
            }

            await _dbContext.SaveChangesAsync();

            return entity;
        }
        
        protected TEntity SetTimestamps(TEntity entity)
        {
            if (entity.Id <= 0)
            {
                entity.Created = DateTime.UtcNow;
                entity.CreatedBy = AppContextHelper.Current.User.Identity.Name;
            }

            entity.Modified = DateTime.UtcNow;
            entity.ModifiedBy = AppContextHelper.Current.User.Identity.Name;

            return entity;
        }
    }
}
