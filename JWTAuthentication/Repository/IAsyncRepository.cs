using JWTAuthentication.Common;
using JWTAuthentication.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JWTAuthentication.Repository
{
    public interface IAsyncRepository<T> 
        where T : BaseEntity
    {
        T Add(T entity);
        void UpdateAsync(T entity);
        void Delete(T entity);
        IDbContextTransaction BeginTransaction();
        IDbContextTransaction CurrentTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        IQueryable<T> GetByCriteria(Expression<Func<T, bool>> criteria, bool includeAll = false);
        IEnumerable<T> ListAll();
        bool AnyEntity(Expression<Func<T, bool>> criteria);
        Task<T> GetByIdAsync(int id, bool includeAll = false);
        Task DeleteEntity(int entityId);

    }
}
