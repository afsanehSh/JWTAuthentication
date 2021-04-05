using JWTAuthentication.Interfaces;
using JWTAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTAuthentication.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly AppSettingModel _appSetting;
        readonly IAppUserService _appUserService;
        public AuthenticationController(IAppUserService appUserService, IOptions<AppSettingModel> appSetting)
        {
            _appUserService = appUserService;
            _appSetting = appSetting.Value;
        }

        [AllowAnonymous]
        [HttpPost("SignInByUserName")]
        public IActionResult SignInByUserName([FromBody] SignInModel signInModel)
        {
            try
            {
                var model = new AuthInfoModel
                {
                    UserName = signInModel.UserName,
                    Password = signInModel.Password
                };
                var loginInfoIsValid = _appUserService.AuthenticateByUserName(model.UserName, model.Password);

                if (!loginInfoIsValid)
                {
                    return BadRequest("نام کاربری یا رمزعبور صحیح نمی باشد");
                }
                var userInfo = _appUserService.GetUserByName(model.UserName);
                var expiresIn = DateTime.UtcNow.AddHours(4);
                model.UserId = userInfo.Id;
                model.Email = userInfo.Email;
                model.UserName = userInfo.UserName;
                model.FirstName = userInfo.FirstName;
                model.LastName = userInfo.LastName;
                model.IsAdmin = userInfo.IsAdmin;
                model.UserRole = "Admin";
                model.ExpiresIn = expiresIn;
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
