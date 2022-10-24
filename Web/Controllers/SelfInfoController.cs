using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.Cryptographer;
using static Web.Constants.ClaimConstants;
using System.Security.Claims;

namespace Web.Controllers
{
    public class SelfInfoController : Controller
    {

        SelfInfoService _selfInfoService;
        ApiTokenProvider _apiToken;
        ICryptographer _cryptographer;
        public SelfInfoController(SelfInfoService selfInfoService, ApiTokenProvider apiToken, ICryptographer cryptographer)
        {
            _selfInfoService = selfInfoService;
            _apiToken = apiToken;
            _cryptographer = cryptographer;
        }
        [Authorize]
        public async Task<IActionResult> RentSelfInfo()
        {
            var res = await _selfInfoService.GetUserRentSelfInfo(await Token());

            return Json(res);
        }
        [Authorize]
        public async Task<IActionResult> ProfileSelfInfo()
        {
            var res = await _selfInfoService.GetUserProfileSelfInfo(await Token());

            return Json(res);
        }
        async Task<string> Token()
        {
            var encryptPassword = User.Claims.First(t => t.ValueType == CLAIM_PASSWORD).Value;
            var login = User.Claims.First(t => t.ValueType == ClaimsIdentity.DefaultNameClaimType).Value;
            var password = _cryptographer.Decrypt(encryptPassword);
            var token = await _apiToken.GetToken(password, login);
            return token;
        }
    }
}
