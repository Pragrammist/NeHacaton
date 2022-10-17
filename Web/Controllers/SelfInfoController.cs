using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class SelfInfoController : Controller
    {

        SelfInfoService _selfInfoService;
        public SelfInfoController(SelfInfoService selfInfoService)
        {
            _selfInfoService = selfInfoService;
        }

        [Authorize]
        public IActionResult Get()
        {
            return View();
        }
    }
}
