﻿using Mephist.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mephist.Services.DAL
{
    public class GenericRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        private UniversityContext context;

        public GenericRepository(UniversityContext context)
        {
            this.context = context;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public virtual void Remove(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                context.Attach(entity);
            }
            context.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            context.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
