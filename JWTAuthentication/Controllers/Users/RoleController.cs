using JWTAuthentication.Entities;
using JWTAuthentication.Interfaces;
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
    public class RoleController : ControllerBase
    {
        readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }

        [HttpGet("GetRoleList")]
        public ActionResult<IEnumerable<Role>> GetAll()
        {
            var entity = _roleService.GetRoleList().ToList();
            return Ok(entity);
        }

        [HttpGet("GetRoleById/{id}")]
        public async Task<ActionResult> GetRoleById([FromRoute] int id)
        {
            var entity = _roleService.LoadRole(id);
            return Ok(entity);
        }

        [AllowAnonymous]
        [HttpPut("UpdateRole")]
        public ActionResult UpdateRole([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                _roleService.UpdateRole(role);

                return Ok(role);
            }
            else
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("AddRole")]
        public IActionResult CreateRole([FromBody] Role role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.AddRole(role);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteRole(id);
            return NoContent();
        }
    }
}
