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

        //[AllowAnonymous]
        //[HttpPost("SignUpUser")]
        //public IActionResult SignUpUser([FromBody] SignUpViewModel signUpModel)
        //{
        //    var result = _appUserService.SignUpUser(signUpModel);
        //    return ValidationResponse(result);
        //}

        //[AllowAnonymous]
        //[HttpPut("ChangePassword")]
        //public IActionResult ChangePassword([FromBody] PasswordViewModel passwordModel)
        //{
        //    var result = _appUserService.ChangePassword(passwordModel);
        //    return ValidationResponse(result);
        //}

        //[AllowAnonymous]
        //[HttpPost("AddAppUser")]
        //public IActionResult AddAppUser(AppUserDto model)
        //{
        //    var appUser = _appUserService.AddAppUser(model);
        //    return Ok(appUser);
        //}

        //[AllowAnonymous]
        //[HttpPut("UpdateAppUser")]
        //public IActionResult UpdateAppUser(AppUserDto model)
        //{
        //    var result = _appUserService.UpdateAppUser(model);
        //    return ValidationResponse(result);
        //}

        //[HttpPut("UploadUserImage/{id}")]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        //public IActionResult UploadUserImage([FromRoute] int id, [FromForm] IFormFile file)
        //{
        //    _appUserService.UploadUserImage(file, id);
        //    return Ok();
        //}
    }
}
