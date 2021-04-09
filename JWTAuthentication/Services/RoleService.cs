using AutoMapper;
using JWTAuthentication.Common;
using JWTAuthentication.Common.ServiceBase;
using JWTAuthentication.Entities;
using JWTAuthentication.Interfaces;
using JWTAuthentication.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Services
{
    public class RoleService : ServiceBase<Role>, IRoleService
    {
        public RoleService(IAsyncRepository<Role> entityRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(entityRepository, httpContextAccessor, mapper)
        {
        }

        public ValidationResult AddRole(Role entity)
        {

            var submitRes = DoSubmit(entity);
            return submitRes;
        }

        public async Task DeleteRole(int entityId)
        {
            var entity = await LoadEntity(entityId);
            EntityRepository.Delete(entity);
        }
        public IEnumerable<Role> GetRoleList()
        {
            return EntityRepository.ListAll().ToList();
        }

        public Role LoadRole(int roleId)
        {
            return EntityRepository.GetByCriteria(x => x.Id == roleId).FirstOrDefault();
        }

        public ValidationResult UpdateRole(Role entity)
        {
            var role = Mapper.Map<Role>(entity);
            var updateRole = DoSubmit(role);
            return updateRole;
        }
    }
}
