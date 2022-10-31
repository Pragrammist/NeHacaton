using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.Cryptography;
using static Web.Constants.ClaimConstants;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;
using Web.Dtos.UserSelfInfoDto.Profile;

namespace Web.Controllers
{
    public class ProfileController : Controller
    {
        SelfInfoService _selfInfoService;
        ApiTokenProvider _apiToken;
        ICryptographer _cryptographer;
        public ProfileController(SelfInfoService selfInfoService, ApiTokenProvider apiToken, ICryptographer cryptographer)
        {
            _selfInfoService = selfInfoService;
            _apiToken = apiToken;
            _cryptographer = cryptographer;
        }
        [Authorize]
        public async Task<IActionResult> Rent()
        {
            var res = await _selfInfoService.GetUserRent(await Token());

            return Json(res);
        }
        [Authorize]
        public async Task<IActionResult> Info()
        {
            var res = await _selfInfoService.GetUserProfile(await Token(), Login);
            res.Array = MakeJustOneProfile(res);
            return Json(res);
        }
        List<OutputProfileDto> MakeJustOneProfile(OutputProfileResultDto rent) => rent.Array.Take(1).ToList();



        [Authorize]
        [HttpPut]
        public async Task<IActionResult> City(string city)
        {
            var user = await _selfInfoService.ChangeCity(city, Login);
            return Json(user);
        }

        string Login => User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) ?? throw new NullReferenceException("Cookies doesn't have login.");

        string Password => User.FindFirstValue(CLAIM_PASSWORD) ?? throw new NullReferenceException("Cookies doesn't have password.");

        async Task<string> Token() => await _apiToken.GetTokenFrom(Password, Login);
        
        
    }
}
