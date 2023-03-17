using BlogShared.Data.Abstract;
using BlogShared.Data.Entities.Abstract;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogShared.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        protected readonly DbContext _context;

        public EfEntityRepositoryBase(DbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate=null)
        {
            return await (predicate == null ? _context.Set<T>().CountAsync() : _context.Set<T>().CountAsync(predicate));
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { _context.Set<T>().Remove(entity); });
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        
        {
            IQueryable<T> query = _context.Set<T>();

            if(predicate != null)
            {
                query = query.Where(predicate);
            }
            
            if (includeProperties.Any())
            {
                foreach(var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IList<T>> GetAllAsyncV2(IList<Expression<Func<T, bool>>> predicate, IList<Expression<Func<T, object>>> includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null && predicate.Any())
            {
                foreach (var item in predicate)
                {
                    query = query.Where(item);
                }
            }
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public IQueryable<T> GetAsQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<T> GetAsyncV2(IList<Expression<Func<T, bool>>> predicate, IList<Expression<Func<T, object>>> includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null && predicate.Any())
            {
                foreach(var item in predicate)
                {
                    query = query.Where(item);
                }
            }
            if (includeProperties != null && includeProperties.Any())
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return await query.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<IList<T>> SearchAsync(IList<Expression<Func<T, bool>>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate.Any())
            {
                var predicateChain = PredicateBuilder.New<T>();
                foreach (var predicates in predicate)
                {
                    predicateChain.Or(predicates);
                }
                query= query.Where(predicateChain);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
           await Task.Run(() => { _context.Set<T>().Update(entity); });
           return entity;
        }
    }
}
