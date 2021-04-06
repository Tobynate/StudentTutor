using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentTutorApi.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TutorsNetworkApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;

        public UserController(IConfiguration config)
        {
            this._config = config;
        }
        [HttpGet]
        public IActionResult GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData(_config);

            return Ok(data.GetUserById(userId));
        }

        [HttpGet]
        [Route("GetId")]
        public IActionResult GetId()
        {
            return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
