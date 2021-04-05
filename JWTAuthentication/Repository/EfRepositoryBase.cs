using AutoMapper;
using AutoMapper.QueryableExtensions;
using JWTAuthentication.Common;
using JWTAuthentication.Entities;
using JWTAuthentication.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JWTAuthentication.Repository
{
    public class EfRepositoryBase<T, TDBContext> : IAsyncRepository<T>
        where T : BaseEntity
        where TDBContext : DbContext
    {
        protected readonly DbContext _dbContext;
        protected readonly IMapper _mapper;
        public IHttpContextAccessor HttpContextAccessor { get; }
        protected readonly DbSet<T> _dataset;
        public EfRepositoryBase(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _dataset = _dbContext.Set<T>();
            _mapper = mapper;
        }

        public T Add(T entity)
        {
            try
            {
                _dataset.Add(entity);
                _dbContext.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public IDbContextTransaction BeginTransaction()
        {
            if (_dbContext.Database.CurrentTransaction == null)
            {
                return _dbContext.Database.BeginTransaction();
            }
            else
            {
                return null;
            }
        }

        public IDbContextTransaction CurrentTransaction()
        {
            return _dbContext.Database.CurrentTransaction;
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public bool AnyEntity(Expression<Func<T, bool>> criteria)
        {
            return _dataset.Any(criteria);
        }

        public IEnumerable<T> ListAll()
        {
            return _dataset.ToList();
        }

        public IQueryable<T> GetByCriteria(Expression<Func<T, bool>> criteria, bool includeAll = false)
        {
            var query = _dataset.AsQueryable();
            if (includeAll)
            {
                var navigations = _dbContext.Model.FindEntityType(typeof(T))
                    .GetDerivedTypesInclusive()
                    .SelectMany(type => type.GetNavigations())
                    .Distinct();

                foreach (var property in navigations)
                    query = query.Include(property.Name);
            }
            return query.Where(criteria);
        }


    }
}
