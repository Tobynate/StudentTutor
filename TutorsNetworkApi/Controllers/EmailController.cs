using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TutorsNetworkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public EmailController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
         private async Task<IdentityUser> CurrentUser()
        {
            var output = await _userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return output;
        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ObjectResult("Error, user is null"); ;
            }

            var result = await _userManager.ConfirmEmailAsync(await CurrentUser(), token);

            ConfirmEmailViewController confirmEmail = new ConfirmEmailViewController();
            await confirmEmail.ShowView(result);
            return Ok();
            //return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}
