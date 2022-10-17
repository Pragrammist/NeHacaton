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
        public IActionResult GetSelfInfo()
        {
            return View(); //all info
        }

        [Authorize]
        public IActionResult RentSelfInfo()
        {
            return PartialView(); //loaded from GetSelfInfo
        }

        [Authorize]
        public IActionResult ProfileSelfInfo()
        {
            return PartialView(); //loaded from GetSelfInfo
        }
    }
}
