using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatApp.SRMDbContexts.IRepository
{
    public interface IGenericRepository<TEntity, in TKey> where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(TKey id);

        Task<EntityEntry<TEntity>> Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IQueryable<TEntity> Find();
        ICollection<TType> Select<TType>(Expression<Func<TEntity, TType>> select) where TType : class;
    }
}

