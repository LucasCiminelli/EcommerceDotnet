using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Persistence;
using Ecommerce.Application.Specifications;
using Infrastructure.Persistence;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : class
    {

        protected readonly EcommerceDbContext _context;



        public RepositoryBase(EcommerceDbContext context)
        {
            _context = context;
        }


        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity); //solo agregarlo en memoria
        }

        public void AddRange(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }



        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IReadOnlyList<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }



        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeString, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();

        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return (await _context.Set<T>().FindAsync(id))!;
        }



        public async Task<T> GetEntityAsync(Expression<Func<T, bool>>? predicate, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            return (await query.FirstOrDefaultAsync())!;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return (await ApplySpecification(spec).FirstOrDefaultAsync())!;
        }
        public async Task<IReadOnlyList<T>> GetAllWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }


        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }

    }
}