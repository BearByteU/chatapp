using ChatApp.SRMDbContexts.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatApp.SRMDbContexts.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        internal ChatAppDbContext _context;
        internal DbSet<TEntity> dbSet;
        public GenericRepository(ChatAppDbContext context)
        {
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return QFind(filter, orderBy, includeProperties);
        }

        public IQueryable<TEntity> Find()
        {
            IQueryable<TEntity> query = dbSet;
            return query;
        }

        public IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<EntityEntry<TEntity>> Insert(TEntity entity)
        {
            return await dbSet.AddAsync(entity);
        }
        public ICollection<TType> Select<TType>(Expression<Func<TEntity, TType>> select) where TType : class
        {
            return dbSet.Select(select).ToList();
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<TEntity> QFind(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }


            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }
    }
}
