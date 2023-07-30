using CleanArchitectureSample.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanArchitectureSample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}