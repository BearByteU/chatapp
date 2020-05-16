using ChatApp.Constants;
using ChatApp.ISerivce;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TaskQ.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _authenticate;
        public LoginController(ILoginService authenticate)
        {
            _authenticate = authenticate;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult> FetchLogin([FromBody]LoginDto logindto)
        {            
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _authenticate.FetchLoginCredential(logindto);
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