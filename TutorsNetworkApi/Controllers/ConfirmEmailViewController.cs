using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorsNetworkApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ConfirmEmailViewController : ControllerBase
    {
        // GET: ConfirmEmailView
        public async Task<IActionResult> ShowView(IdentityResult result)
        {
            return Ok(); //(IActionResult)View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}
