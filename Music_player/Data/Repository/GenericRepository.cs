using Microsoft.EntityFrameworkCore;
using Music_player.Data;
using Music_player.Data.Entity;
using Music_player.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_player.Data.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly MusicDbContext _dbContext;

        public GenericRepository(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var result = await _dbContext.Set<TEntity>()
                .AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            var result = _dbContext.Set<TEntity>()
                .Update(entity);

            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity 
        {
            return _dbContext.Set<TEntity>()
                .Select(i => i);
        }
    }
}