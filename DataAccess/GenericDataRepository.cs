using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Wags.DataModel;

namespace Wags.DataAccess
{
    /// <summary>
    /// A generic data access layer (DAL) with full CRUD (Create, Read, Update and Delete) support using Entity Framework 5 with plain old CLR objects (POCOs)
    /// and short-lived contexts in a disconnected and stateless N-tier application.
    /// based on ideas in http://blog.magnusmontin.net/2013/05/30/generic-dal-using-entity-framework/
    /// </summary>
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class, IEntity
    {
        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new ModelContainer())
            {
                IQueryable<T> dbQuery = context.Set<T>();
 
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
                 
                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            }
            return list;
        }
 
        public virtual IList<T> GetList(Expression<Func<T, bool>> where, 
             params Expression<Func<T,object>>[] navigationProperties)
        {
            List<T> list;
            using (var context = new ModelContainer())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                 
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
 
                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            }
            return list;
        }
 
        public virtual T GetSingle(Expression<Func<T, bool>> where,
             params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            using (var context = new ModelContainer())
            {
                IQueryable<T> dbQuery = context.Set<T>();
                 
                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);
 
                item = dbQuery
                    .AsNoTracking() //Don't track any changes for the selected item
                    .FirstOrDefault(where); //Apply where clause
            }
            return item;
        }

        public virtual void Add(params T[] items)
        {
            Update(items);           
        }

        public virtual void Update(params T[] items)
        {
            using (var context = new ModelContainer())
            {
                DbSet<T> dbSet = context.Set<T>();
                foreach (T item in items)
                {
                    dbSet.Add(item);
                    foreach (DbEntityEntry<IEntity> entry in context.ChangeTracker.Entries<IEntity>())
                    {
                        IEntity entity = entry.Entity;
                        entry.State = GetEntityState(entity.EntityState);
                    }
                }
                ////For some reason Relationship entities are set to state "added" which gives a multiplicity constraint error
                //var stateManager = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager;
                //var added = stateManager.GetObjectStateEntries(System.Data.Entity.EntityState.Added).ToList();
                //foreach (var objectStateEntry in added.Where(o => o.IsRelationship))
                //{
                //        objectStateEntry.ChangeState(System.Data.Entity.EntityState.Unchanged);
                //}
                context.SaveChanges();
            }
        }

        public virtual void Remove(params T[] items)
        {
            Update(items);
        }

        //Helper to convert between custom DataModel EntityState and System.Data.Entity.EntityState
        protected static System.Data.Entity.EntityState GetEntityState(DataModel.EntityState entityState)
        {
            switch (entityState)
            {
                case DataModel.EntityState.Unchanged:
                    return System.Data.Entity.EntityState.Unchanged;
                case DataModel.EntityState.Added:
                    return System.Data.Entity.EntityState.Added;
                case DataModel.EntityState.Modified:
                    return System.Data.Entity.EntityState.Modified;
                case DataModel.EntityState.Deleted:
                    return System.Data.Entity.EntityState.Deleted;
                default:
                    return System.Data.Entity.EntityState.Detached;
            }
        }
    }
}