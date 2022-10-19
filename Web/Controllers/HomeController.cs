using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using HendInRentApi;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        AuthRentInHendApi _authIn;

        public HomeController(ILogger<HomeController> logger, AuthRentInHendApi authIn)
        {
            _logger = logger;
            _authIn = authIn; 
        }

        public async Task <IActionResult> Index()
        {
            return View();   
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
