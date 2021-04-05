using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Entities
{
    public class Role: BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleTitle { get; set; }
        public ICollection<UserRole> UserRoleList { get; set; }
    }
}
