using Microsoft.AspNet.Identity.Owin;
using StudentTutorApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentTutorApi.Controllers
{
    public class EmailController : ApiController
    {
        private ApplicationUserManager  _userManager;
        public EmailController(ApplicationUserManager usrMgr)
        {
            _userManager = usrMgr;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public async Task<IHttpActionResult> ConfirmEmail(string token, string email)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Ok<string>("Error, user is null"); ;
            }

            var result = await UserManager.ConfirmEmailAsync(user.Id, token);

            ConfirmEmailViewController confirmEmail = new ConfirmEmailViewController();
            await confirmEmail.ShowView(result);
            return Ok();
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}
