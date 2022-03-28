using DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Repository.Data.Abstraction
{
    public abstract class EFCoreRepository<TEntity, TEntityPrimaryKey, TEntityDto, TContext> : IRepository<TEntity, TEntityPrimaryKey>
        where TEntity : class
        where TContext : DbContext
        where TEntityDto : class
    {
        private readonly TContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFCoreRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(TEntityPrimaryKey id)
        {
            TEntity entity = await _dbSet.FindAsync(id);

            if (entity == null) return null;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;

        }

        public async Task<TEntity> Get(TEntityPrimaryKey id)
        {
            TEntity entity = await _dbSet.FindAsync(id);

            if (entity == null) return null;

            return entity;

        }

        public async Task<List<TEntity>> GetAll()
        {
            List<TEntity> entities = await _dbSet.ToListAsync();

            return entities;
        }

        public async Task<TEntity> Update(TEntityPrimaryKey id, TEntity entity)
        {
            var _entity = await _context.Set<TEntity>().FindAsync(id);
            var updated = RepoUtils.CheckUpdateObject(_entity, entity);
            _context.Entry(_entity).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();


            return entity;
        }

    }
}
