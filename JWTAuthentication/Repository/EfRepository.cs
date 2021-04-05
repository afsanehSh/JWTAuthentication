using AutoMapper;
using JWTAuthentication.Data;
using JWTAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Repository
{
    public class EfRepository<T> : EfRepositoryBase<T, ApplicationDbContext>
         where T : BaseEntity
    {

        public EfRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
