using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class BaseAuthInfo
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool? IsAdmin { get; set; }
        public string UserRole { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }
}
