using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iWasHere.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult ListOfCategories()
        {
            return View();
        }

        public IActionResult Random()
        {

            return View();
        }
    }
}