using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using StudentTutorApi.Library.DataAccess;
using StudentTutorApi.Library.Models;
using StudentTutorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace StudentTutorApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
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

        [HttpGet]
        [Route("GetId")]
        public IHttpActionResult GetId()
        {
            return Ok(RequestContext.Principal.Identity.GetUserId());
        }

        //[HttpPost]
        //[Route("DefaultRoleToAdd")]
        //public async Task<IHttpActionResult> DefaultRoleAdd(string userId)
        //{
        //    AccountController account = new AccountController();
        //    if (account.UserManager.GetRolesAsync(userId).Result?.Count > 0)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //       // await AddToRoleLogic(userId, "STUDENT");
        //    }
        //    return Ok();
        //}
       
    }
}
