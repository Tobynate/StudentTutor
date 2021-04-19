using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentTutorApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TutorsNetworkApi.Data;
using TutorsNetworkApi.Email;
using TutorsNetworkApi.Models;

namespace TutorsNetworkApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private const string LocalLoginProvider = "Local";
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private UserManager<IdentityUser> _userManager;
         
        public AccountController()
        {
        }

        public AccountController(UserManager<IdentityUser> userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat, ApplicationDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            AccessTokenFormat = accessTokenFormat;
            _context = context;
            this._config = configuration;
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        
        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        [HttpPut]
        public async Task<IActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.ChangePasswordAsync(await CurrentUser(), model.OldPassword,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        [Authorize(Roles = "ADMIN")]
        [HttpPut]
        public async Task<IActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.AddPasswordAsync(await CurrentUser(), model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        private async Task<IdentityUser> CurrentUser()
        {
            var output = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return output;
        }
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterBindingModel model, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser() { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            var IsSucess = await ConfirmEmailLogic(user, password);

            return Ok();
        }

        private async Task<IActionResult> ConfirmEmailLogic(IdentityUser user, string password)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Link("Default", new { Controller = "Email", Action = "ConfirmEmail", token, email = user.Email });
            EmailHelper emailHelper = new EmailHelper(_config);
            bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink, password);

            if (emailResponse)
            {
                return Redirect("Index");
            }
            return new ObjectResult(false);
        }
        [Route("ConfirmEmail")]
        [HttpPut]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _userManager.ConfirmEmailAsync(await CurrentUser(), model.AccessToken);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(RoleModel model)
        {
            return Ok(await AddToRoleLogic(model.UserId, model.RoleName));
        }
        
        internal async Task<IActionResult> AddToRoleLogic(string userId, string roleName)
        {           
            //var userStore = new UserStore<ApplicationUser>(_context);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            if (/*!RequestContext.Principal.IsInRole*/!User.IsInRole(roleName))
            {
                IdentityResult result = await _userManager.AddToRoleAsync(await CurrentUser(), roleName);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
            }
            else
            {
                throw new Exception("The User is already in this role.");
            }

            return Ok();
        }
        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            return Ok(await _userManager.GetRolesAsync(await CurrentUser()));
        }

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing && _userManager != null)
        //    {
        //        _userManager.Dispose();
        //        _userManager = null;
        //    }
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //}
        #region Helpers

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
        #endregion
    }
}
