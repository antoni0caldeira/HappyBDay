using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Controllers
{
    public class OptionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
