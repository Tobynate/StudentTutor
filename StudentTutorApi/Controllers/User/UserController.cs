using Microsoft.AspNet.Identity;
using StudentTutorApi.Library.DataAccess;
using StudentTutorApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace StudentTutorApi.Controllers
{
    [Authorize]
    //[RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            //List<UserModel> user = );

            return Ok(data.GetUserById(userId));
        }
        
    }
}
