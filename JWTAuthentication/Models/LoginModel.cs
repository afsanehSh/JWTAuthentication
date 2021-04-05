using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Models
{
    public class AuthInfoModel : BaseAuthInfo
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserTitle => FirstName + " " + LastName;
    }

    public class SignInModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
