using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
    public interface IRepository<TEntity, TKey, TContext>
    where TEntity : class, IEntity<TKey>
    where TContext : DbContext
    {
        Task AddAsync(TEntity entity);

        Task RemoveAsync(TKey id);

        Task RemoveAsync(TEntity entityToDelete);

        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);

        Task EditAsync(TEntity entityToUpdate);

        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);

        Task<IList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);

        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TKey id);

        Task<(IList<TEntity> data, int total, int totalDisplay)> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false);

        Task<(IList<TEntity> data, int total, int totalDisplay)> GetDynamicAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false);

        Task<IList<TEntity>> GetAsync(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false);

        Task<IList<TEntity>> GetDynamicAsync(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false);

        void Add(TEntity entity);

        void Remove(TKey id);

        void Remove(TEntity entityToDelete);

        void Remove(Expression<Func<TEntity, bool>> filter);

        void Edit(TEntity entityToUpdate);

        int GetCount(Expression<Func<TEntity, bool>> filter = null);

        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter);

        IList<TEntity> GetAll();

        TEntity GetById(TKey id);

        (IList<TEntity> data, int total, int totalDisplay) Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false);

        (IList<TEntity> data, int total, int totalDisplay) GetDynamic(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          int pageIndex = 1,
          int pageSize = 10,
          bool isTrackingOff = false);

        IList<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false);

        IList<TEntity> GetDynamic(
          Expression<Func<TEntity, bool>> filter = null,
          string orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
          bool isTrackingOff = false);
    }
}