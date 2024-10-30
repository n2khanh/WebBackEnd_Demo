using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zoo_template.Models;

namespace Zoo_template.Controllers
{
    public class HomeCustomer : Controller
    {
        public IActionResult Index()
        {
            return View("CustomerIndex");
        }
        public IActionResult Order()
        {
            return View();
        }
    }
}
