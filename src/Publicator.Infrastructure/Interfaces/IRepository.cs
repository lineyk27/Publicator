using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Publicator.Infrastructure.Interfaces
{
    interface IRepository<TEntity>
        where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
                                Func<IQueryable<TEntity>,
                                IOrderedQueryable<TEntity>> orderBy = null,
                                string includeProperties = "");
        Task<TEntity> GetByIdAsync(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
    }
}
