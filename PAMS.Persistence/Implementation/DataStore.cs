﻿using Microsoft.EntityFrameworkCore;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PAMS.Persistence.Implementation
{
    public class DataStore<T> : IDataStore<T> where T : class
    {
        protected readonly PAMSdbContext context;
        DbSet<T> table;
        public DataStore(PAMSdbContext pAMSdbContext)
        {
            this.context = pAMSdbContext;
            table = context.Set<T>();
        }
        public async Task Add(T entity)
        {
            await table.AddAsync(entity);
        }

        public async Task AddRange(List<T> entities)
        {
            await table.AddRangeAsync(entities);
        }

        public async Task<bool> Delete(Guid ID)
        {
            var entity = await GetById(ID);
            if (entity != null)
            {
                table.Remove(entity);
                return true;
            }
            return false;

        }

        public async Task<bool> Delete(long Id)
        {
            var entity = await GetById(Id);
            if (entity != null)
            {
                table.Remove(entity);
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await table.AsNoTracking().ToListAsync();
        }

        public IQueryable<T> GetAllQuery()
        {
            return table.AsQueryable();
        }

        public async Task<T> GetById(Guid ID)
        {
            return await table.FindAsync(ID);
        }

        public async Task<T> GetById(long Id)
        {
            return await table.FindAsync(Id);
        }

        public void Update(T entity)
        {
            table.Update(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(List<T> entities)
        {
            table.UpdateRange(entities);
            context.Entry(entities).State = EntityState.Modified;
        }
    }
}
