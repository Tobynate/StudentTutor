using Microsoft.AspNet.Identity;
using StudentTutorApi.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentTutorApi.Controllers.User
{
    [RoutePrefix("api/User/Passport_Update")]
    public class PassportController : ApiController
    {
        [HttpPut]
        public IHttpActionResult UpdateUserPassport(byte[] passport)
        {
            //Image img = Image.FromFile("C:\\Users\\USER\\Pictures\\Tigerwoods.jpg");
            //byte[] getImg = ConvertImgToByte(img);

            string userId = RequestContext.Principal.Identity.GetUserId();

            UserData data = new UserData();
            data.UpdatePassport(userId, passport);

            return Ok();

        }
        //public static byte[] ConvertImgToByte(Image image)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    image.Save(ms, image.RawFormat);
        //    byte[] output = ms.ToArray();

        //    return output;
        //}
    }
}
