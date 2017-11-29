﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using VolunteerSystem.Database.DatabaseInterfaces;

namespace VolunteerSystem.Database
{
    public class Database<TEntity> : IDatabaseSkov<TEntity> where TEntity : class, IDisposable
        {
            protected readonly DbContext Context;

            public Database(DbContext context)
            { 
                this.Context = context;
            }
            public void Add(TEntity entity)
            {
                Context.Set<TEntity>().Add(entity);
            }

            public void AddRange(IEnumerable<TEntity> entities)
            {
                Context.Set<TEntity>().AddRange(entities);
            }

            public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
            {
                return Context.Set<TEntity>().Where(predicate);
            }

            public TEntity Get(int Id)
            {
                return Context.Set<TEntity>().Find(Id);
            }

            public IEnumerable<TEntity> GetAll()
            {
                return Context.Set<TEntity>().ToList();
            }

            public void Remove(TEntity entity)
            {
                Context.Set<TEntity>().Remove(entity);
            }

            public void RemoveRange(IEnumerable<TEntity> entities)
            {
                Context.Set<TEntity>().RemoveRange(entities);
            }
            public int Complete()
            {
                return Context.SaveChanges();
            }
            public void Dispose()
            {
                Context.Dispose();
            }
        
    }
}
