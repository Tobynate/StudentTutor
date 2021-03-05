using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentTutorApi.Controllers
{
    public class ConfirmEmailViewController : Controller
    {
        // GET: ConfirmEmailView
        public async Task<IActionResult> ShowView(IdentityResult result)
        {
            return (IActionResult)View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
    }
}