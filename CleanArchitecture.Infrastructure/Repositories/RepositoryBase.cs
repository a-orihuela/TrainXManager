﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Specification;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly CADbContext _context;

        public RepositoryBase(CADbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        //public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        //                               Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //                               string includeString = null,
        //                               bool disableTracking = true)
        //{
        //    IQueryable<T> query = _context.Set<T>();
        //    if (disableTracking) query = query.AsNoTracking();
        //    if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
        //    if (predicate != null) query = query.Where(predicate);
        //    if (orderBy != null)
        //        return await orderBy(query).ToListAsync();
        //    return await query.ToListAsync();
        //}

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     List<Expression<Func<T, object>>> includes = null,
                                     bool disableTracking = true)
        {
            IQueryable<T> query = _context.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        //public async Task<T> AddAsync(T entity)
        //{
        //    _context.Set<T>().Add(entity);
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task<T> UpdateAsync(T entity)
        //{
        //    _context.Set<T>().Attach(entity);
        //    _context.Entry(entity).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //    return entity;
        //}

        //public async Task DeleteAsync(T entity)
        //{
        //    _context.Set<T>().Remove(entity);
        //    await _context.SaveChangesAsync();
        //}

        public void AddEntity(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void UpdateEntity(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
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
