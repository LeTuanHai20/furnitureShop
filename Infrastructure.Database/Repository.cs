using Infrastructure.Database;
using Infrastructure.Database.Entities;
using Infrastructure.Database.Models;
using Infrastructure.Database.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
    public class Repository<Tcontext, TEntity> : BaseRepository<Tcontext, TEntity>, IRepository<TEntity>
        where TEntity : class
        where Tcontext : DbContext, new()
    {
        IUnitOfWork<Tcontext> unitOfWork;
        public Repository(IUnitOfWork<Tcontext> unitOfWork) : base(unitOfWork._context)
        {
            this.unitOfWork = unitOfWork;
        }

        public TEntity Get(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }


        public void Add(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void BulkInsert(IList<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            //throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            if (entity != null) _dbSet.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity != null) _dbSet.Remove(entity);
        }


        public void Delete(object id)
        {
            var typeInfo = typeof(TEntity).GetTypeInfo();
            var key = _dbContext.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
            var property = typeInfo.GetProperty(key?.Name);
            if (property != null)
            {
                var entity = Activator.CreateInstance<TEntity>();
                property.SetValue(entity, id);
                _dbContext.Entry(entity).State = EntityState.Deleted;
            }
            else
            {
                var entity = _dbSet.Find(id);
                if (entity != null) Delete(entity);
            }
        }

        public void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }


        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsEnumerable();
        }

        [Obsolete("Method is replaced by GetList")]
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsEnumerable();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }


        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public Task<IPaginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int index = 0,
            int size = 20,
            bool disableTracking = true,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).ToPaginateAsync(index, size, 0, cancellationToken);
            return query.ToPaginateAsync(index, size, 0, cancellationToken);
        }

        public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        public Task AddAsync(params TEntity[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }


        public Task AddAsync(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddRangeAsync(entities, cancellationToken);
        }


        [Obsolete("Use get list ")]
        public IEnumerable<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return AddAsync(entity, new CancellationToken());
        }

        public void BulkDelete(IList<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            //throw new NotImplementedException();
        }

        public int Save(RequestContext requestContext = null)
        {
            return unitOfWork.Save(requestContext);
        }

        public Task SaveAsync(RequestContext requestContext = null)
        {
            return unitOfWork.SaveAsync(requestContext);
        }

		public IDbContextTransaction BeginTransaction()
		{
            return unitOfWork.BeginTransaction();
		}
	}
}
