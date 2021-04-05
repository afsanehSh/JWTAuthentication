using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Entities
{
    public class UserRole: BaseEntity
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("RoleId")]
        public Role RoleEntity { get; set; }
        [ForeignKey("UserId")]
        public AppUser UserEntity { get; set; }
    }
}
