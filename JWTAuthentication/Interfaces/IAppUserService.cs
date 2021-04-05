using JWTAuthentication.Common;
using JWTAuthentication.Entities;
using JWTAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Interfaces
{
    public interface IAppUserService
    {
        bool AuthenticateByUserName(string userName, string password);
        AppUser GetUserByName(string userName);
        AppUser GetCurrentUser();
        //string GetUserProfileImage(string userName = "");
        //string GetUserImageById(int userId);
        //ValidationResult SignUpUser(SignUpViewModel signUpModel);
        //ValidationResult ChangePassword(PasswordViewModel passwordModel);
        //ValidationResult AddAppUser(AppUserModel model);
        //ValidationResult UpdateAppUser(AppUserModel model);
    }
}
