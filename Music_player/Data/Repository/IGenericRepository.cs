using Music_player.Data;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using Music_player.Data.Entity;
using System.Linq;

namespace Music_player.Data.Repository
{
    public interface IGenericRepository 
    {
        public Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IEntity;
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity;
    }
}