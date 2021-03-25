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
    public class PassportController : ControllerBase
    {
        [HttpPut]
        public IActionResult UpdateUserPassport(byte[] passport)
        {
            //Image img = Image.FromFile("C:\\Users\\USER\\Pictures\\Tigerwoods.jpg");
            //byte[] getImg = ConvertImgToByte(img);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            UserData data = new UserData();
            data.UpdatePassport(userId, passport);

            return Ok();

        }
    }
}
