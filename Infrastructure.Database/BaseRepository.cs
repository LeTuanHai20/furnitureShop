using Infrastructure.Database;
using Infrastructure.Database.DynamicLinq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Database
{
    public abstract class BaseRepository<Tcontext, T> : IReadRepository<T>
        where T : class
        where Tcontext : DbContext, new()
    {
        protected readonly Tcontext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(Tcontext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> All
        {
            get
            {
                return _dbSet;
            }
        }

        public virtual IQueryable<T> Query(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

        public T Search(params object[] keyValues) => _dbSet.Find(keyValues);


        public T Single(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();
            return query.FirstOrDefault();
        }


        public IQueryable<T> List(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query);
            return query;
        }

        public DataSourceResult<T> GetPaged(DataSourceRequest request,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = List(predicate, orderBy, include);
            return query.ToDataSourceResult(request);
        }

		public DatatableResult<T> GetDatatableResult(DatatableRequest request,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool disableTracking = true)
		{
            IQueryable<T> query = List(predicate, orderBy, include, disableTracking);
            return query.ToDatatableResult(request);
        }
	}
}
