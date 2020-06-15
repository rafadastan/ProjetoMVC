using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Projeto.Presentation.Mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index() //nome da página html
        {
            return View();
        }
    }
}