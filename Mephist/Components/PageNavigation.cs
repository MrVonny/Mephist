using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mephist.Components
{
    public class PageNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string action, string controller, int page, int maxPage)
        {
            ViewBag.Action = action;
            ViewBag.Controller = controller;
            ViewBag.Page = page;
            ViewBag.MaxPage = maxPage;
            return View();
        }
    }
}
