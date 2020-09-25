using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mephist.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mephist.Controllers
{
    public class TeachersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddTeacher()
        {
            
            return View();
        }
    }
}
