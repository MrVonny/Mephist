using Mephist.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist
{
    public class EmployeeProfile : ViewComponent
    {
        public EmployeeProfile()
        {

        }

        public IViewComponentResult Invoke(Employee employee)
        {
            
            return View("SmallProfile",employee);
        }
    }
}
