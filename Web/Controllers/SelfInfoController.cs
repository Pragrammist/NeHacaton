using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using static Web.Constants.ClaimConstants;

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
        public async Task<IActionResult> RentSelfInfo()
        {
            var res = await _selfInfoService.GetUserRentSelfInfo(Token);

            return PartialView(res); 
        }

        [Authorize]
        public async Task<IActionResult> ProfileSelfInfo()
        {
            var res = await _selfInfoService.GetUserProfileSelfInfo(Token);

            return PartialView(res); 
        }



        string Token => User.Claims.First(t => t.ValueType == RENTINHEND_API_TOKEN_CLAIM).Value;
    }
}
