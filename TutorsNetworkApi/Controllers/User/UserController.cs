using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentTutorApi.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TutorsNetworkApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData();

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
