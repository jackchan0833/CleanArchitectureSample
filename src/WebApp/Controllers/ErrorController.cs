using CleanArchitectureSample.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CleanArchitectureSample.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        public ErrorController()
        {
        }

        public IActionResult Index(string code)
        {
            var model = new ErrorViewModel();
            model.ErrorCode = code;
            return View("Error");
        }
    }
}