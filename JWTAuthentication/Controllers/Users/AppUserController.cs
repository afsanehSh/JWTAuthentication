using JWTAuthentication.Common;
using JWTAuthentication.Entities;
using JWTAuthentication.Interfaces;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        readonly IAppUserService _appUserService;
        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        [HttpGet("GetCurrentUser")]
        public ActionResult<AppUserModel> GetCurrentUser()
        {
            var result = _appUserService.GetCurrentUser();
            return Ok(result);
        }

        //[HttpGet("GetUserProfileImage")]
        //public async Task<IActionResult> GetUserProfileImage()
        //{
        //    string currentUser = _appUserService.GetCurrentUser().UserName;
        //    var userProfile = _appUserService.GetUserProfileImage(currentUser);
        //    if (string.IsNullOrEmpty(userProfile))
        //    {
        //        return NoContent();
        //    }
        //    var profilePhoto = await DownloadFile(userProfile, ContentFileHelper.UserProfilePicture);
        //    return profilePhoto;
        //}

        //[HttpGet("GetUserImage/{UserId}")]
        //public async Task<IActionResult> GetUserImage(int userId)
        //{
        //    var userProfile = _appUserService.GetUserImageById(userId);
        //    if (string.IsNullOrEmpty(userProfile))
        //    {
        //        return BadRequest("Image not found!");
        //    }
        //    var profilePhoto = await DownloadFile(userProfile, ContentFileHelper.UserProfilePicture);
        //    return profilePhoto;
        //}

        [AllowAnonymous]
        [HttpPost("SignUpUser")]
        public IActionResult SignUpUser([FromBody] SignUpViewModel signUpModel)
        {
            var result = _appUserService.SignUpUser(signUpModel);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword([FromBody] PasswordViewModel passwordModel)
        {
            var result = _appUserService.ChangePassword(passwordModel);
            return Ok(result);
        }

        [HttpGet()]
        public ActionResult<IEnumerable<AppUser>> GetAll()
        {
            var entity = _appUserService.GetAppUserList().ToList();
            return Ok(entity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAppUserById([FromRoute] int id)
        {
            var entity = _appUserService.LoadUser(id);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpPost()]
        public IActionResult AddAppUser(AppUserModel model)
        {
            var appUser = _appUserService.AddAppUser(model);
            return Ok(appUser);
        }

        [AllowAnonymous]
        [HttpPut()]
        public IActionResult UpdateAppUser(AppUserModel model)
        {
            var result = _appUserService.UpdateAppUser(model);
            return Ok(result);
        }

        //[HttpPut("UploadUserImage/{id}")]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //public IActionResult UploadUserImage([FromRoute] int id, [FromForm] IFormFile file)
        //{
        //    _appUserService.UploadUserImage(file, id);
        //    return Ok();
        //}

    }
}
