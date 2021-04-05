using AutoMapper;
using JWTAuthentication.Common;
using JWTAuthentication.Common.ServiceBase;
using JWTAuthentication.Data;
using JWTAuthentication.Entities;
using JWTAuthentication.Interfaces;
using JWTAuthentication.Models;
using JWTAuthentication.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Services
{
    public class AppUserService : ServiceBase<AppUser>, IAppUserService
    {
        public AppUserService(IAsyncRepository<AppUser> entityRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(entityRepository, httpContextAccessor, mapper)
        {
        }
        public bool AuthenticateByUserName(string userName, string password)
        {
            return EntityRepository.AnyEntity(x => x.UserName == userName && x.Password == password);
        }

        public AppUser GetUserByName(string userName)
        {
            var userInfo = EntityRepository.GetByCriteria(n=> n.UserName == userName).FirstOrDefault();
            return userInfo;
        }

         public AppUser GetCurrentUser()
        {
            return GetUserByName(HttpContextAccessor.HttpContext.User.Identity.Name);
        }

        //public string GetUserProfileImage(string userName = "")
        //{
        //    var userProfileImg = _dbContext.Set<AppUser>().Where(x => x.UserName == userName).FirstOrDefault();
        //    return userProfileImg?.UserImage;
        //}
        
        //public string GetUserImageById(int userId)
        //{
        //    var userProfileImg = _dbContext.Set<AppUser>().Where(x => x.Id == userId).FirstOrDefault();
        //    return userProfileImg?.UserImage;
        //}

        //public ValidationResult SignUpUser(SignUpViewModel signUpModel)
        //{
        //    _repository.BeginTransaction();
        //    ValidationResult submitResult = new ValidationResult();
        //    try
        //    {
        //        var user = Mapper.Map<AppUser>(signUpModel);
        //        submitResult = _repository.DoSubmit(user);
        //        if (submitResult.IsOk() && user.IsAdmin == null)
        //        {
        //            //submitResult = AddUserRole(user.Id, AppRoles.User);
                   
        //        }
        //        if (submitResult.IsOk())
        //        {
        //            _repository.CommitTransaction();
        //        }
        //        else
        //        {
        //            _repository.RollbackTransaction();
        //        }
        //    }catch(Exception e)
        //    {

        //    }
        //    return submitResult;
        //}

        //public ValidationResult ChangePassword(PasswordViewModel passwordModel)
        //{
        //    ValidationResult validationResult = new ValidationResult();
        //    var userToChangePass = _dbContext.Set<AppUser>().Where(u => u.UserName == GetCurrentUser().UserName && u.Password == passwordModel.CurrentPassword).FirstOrDefault();
        //    if (userToChangePass != null)
        //    {
        //        userToChangePass.Password = passwordModel.Password;
        //        validationResult = _repository.DoSubmit(userToChangePass);
        //    }
        //    return validationResult;
        //}

        //private ValidationResult AddUserRole(int userId, string roleName)
        //{
        //    var role = _dbContext.Set<Role>().Where(x => x.RoleTitle == roleName).FirstOrDefault().Id;
        //    var userRole = new UserRole()
        //    {
        //        UserId = userId,
        //        RoleId = role
        //    };
        //    var submitResult = _userRoleService.DoSubmit(userRole);
        //    return submitResult;
        //}

        //public ValidationResult AddAppUser(AppUserModel model)
        //{
        //    var appUser = Mapper.Map<AppUser>(model);
        //    var submitRes = _repository.DoSubmit(appUser);
        //    return submitRes;
        //}

        //public ValidationResult UpdateAppUser(AppUserModel model)
        //{
        //    var appUser = Mapper.Map<AppUser>(model);
        //    appUser.UserImage = model.UserImage;
        //    var updateUser = _repository.DoSubmit(appUser);
        //    return updateUser;
        //}

        //public void UploadUserImage(IFormFile file, int id)
        //{
        //    var filePath = FileHelper.SaveFile(file, ContentFileHelper.UserProfilePicture);
        //    var entity = EntityRepository.GetByIdAsync(id).Result;
        //    entity.UserImage = filePath;
        //    _repository.DoSubmit(entity);
        //}
    }
}
