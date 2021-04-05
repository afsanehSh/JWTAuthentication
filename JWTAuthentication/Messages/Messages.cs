using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Messages
{
    public class Messages
    {
        public static string UserOrPassIsNotValid => "نام کاربری یا رمزعبور صحیح نمی باشد";
        public static string ExistsUser => "نام کاربری تکراری می باشد";
        public static string DuplicateMobile => "شماره همراه تکراری می باشد";
    }
}
