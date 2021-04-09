using JWTAuthentication.Common;
using JWTAuthentication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Interfaces
{
    public interface IRoleService
    {
        ValidationResult AddRole(Role entity);
        Task DeleteRole(int entityId);
        IEnumerable<Role> GetRoleList();
        Role LoadRole(int roleId);
        ValidationResult UpdateRole(Role entity);

    }
}
