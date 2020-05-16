using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Constants;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskQ.BAL.Interface;

namespace ChatApp.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Signup")]
        [AllowAnonymous]
        public async Task<ActionResult> SignupUser([FromBody]UserDto logindto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _userService.CreateUser(logindto);
                    if (data == null)
                    {
                        return NotFound(data);
                    }
                    return Ok(data);
                }
                return UnprocessableEntity(new EntityResponseModel
                {
                    Status = false,
                    Message = ConstantString.ValidationError
                });
            }
            catch (Exception)
            {
                return BadRequest(new EntityResponseModel
                {
                    Status = false,
                    Message = ConstantString.InternalSeverError
                });
            }
        }
    }
}