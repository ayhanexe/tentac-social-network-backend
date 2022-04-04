using DomainModels.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> Delete(TEntityPrimaryKey id)
        {
            try
            {
                TEntity entity = await _dbSet.FindAsync(id);

                if (entity == null) return null;

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch(Exception ex)
            {
                return null;
            }

        }

        public virtual async Task<TEntity> Get(TEntityPrimaryKey id)
        {
            TEntity entity = await _dbSet.FindAsync(id);

            if (entity == null) return null;

            return entity;

        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            List<TEntity> entities = await _dbSet.ToListAsync();

            return entities;
        }

        public virtual async Task<TEntity> Update(TEntityPrimaryKey id, TEntity entity)
        {
            var _entity = await _context.Set<TEntity>().FindAsync(id);
            var updated = RepoUtils.CheckUpdateObject(_entity, entity);
            _context.Entry(_entity).CurrentValues.SetValues(updated);
            await _context.SaveChangesAsync();


            return entity;
        }

    }
}
