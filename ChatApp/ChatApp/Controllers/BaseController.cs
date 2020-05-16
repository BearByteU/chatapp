using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public string UserId { 
            get {
               return User.Claims.FirstOrDefault(e => e.Type == "userId").Value; 
            } 
        }
    }
}