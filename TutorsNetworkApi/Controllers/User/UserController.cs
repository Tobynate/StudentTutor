using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentTutorApi.Core.DataAccess;
using StudentTutorApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TutorsNetworkApi.Data;
using TutorsNetworkApi.Email;
using TutorsNetworkApi.Models;

namespace TutorsNetworkApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(IConfiguration config, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this._config = config;
            this._userManager = userManager;
            this._context = context;
        }
        #region
        /// <summary>
        /// Logic for querying User Db. rather than Authentication Db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserData data = new UserData(_config);

            return Ok(data.GetUserById(userId));
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetSubjectOfInterest")]
        public IActionResult GetSubjectOfInterest()
        {
            UserData data = new UserData(_config);

            var i = data.GetSubjectOfInterest();
            return Ok(i);
        }

        #endregion
        [HttpGet]
        [Route("GetId")]
        public IActionResult GetId()
        {
            return Ok(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpPost]
        [Route("DefaultRole_Add")]
        public async Task<IActionResult> AddUserRole()
        {
            var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Name));
            
            if (user != null)
            {
                var roles = _context.Roles;

                foreach (var role in roles)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        return Ok();
                    }
                }
                return Ok(await _userManager.AddToRoleAsync(user, "STUDENT"));
            }

            return Ok();
        }
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(Object userRegistrationModel)
           {
            //var user = new IdentityUser() { UserName = userRegistrationModel.EmailAddress, Email = userRegistrationModel.EmailAddress };
            //var str = userRegistrationModel.EncodedAuthContent.ReadAsStringAsync();
            //IActionResult result = await RegisterUser(user, "password");
            //if (result != Ok())
            //{
            //    return result;
            //}
            //result = await SaveUserData(new UserAccountBindingModel());//TODO fix this
            
            //var IsSucess = await ConfirmEmailLogic(user, "password");

            return Ok();
        }
        private async Task<IActionResult> RegisterUser(IdentityUser user, string password)
        {

            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }
        
        private async Task<IActionResult> SaveUserData(UserAccountBindingModel userAccount)
        {
            UserData data = new UserData(_config);

            return Ok(Task.Run(()=> data.SaveUser(userAccount)));
        }
        private async Task<bool> ConfirmEmailLogic(IdentityUser user, string password)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Link("Default", new { Controller = "Email", Action = "ConfirmEmail", token, email = user.Email });
            EmailHelper emailHelper = new EmailHelper(_config);
            bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink, password);
            
            if (emailResponse)
            {
                Redirect("Index");
                return true;
            }
            return false;
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return StatusCode(500);
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.ToString());
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
