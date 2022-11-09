using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.Cryptography;
using static Web.Constants.ClaimConstants;
using System.Security.Claims;
using Web.Dtos.UserSelfInfoDto.Profile;
using Microsoft.AspNetCore.Http.Extensions;

namespace Web.Controllers
{
    public class ProfileController : Controller
    {
        SelfInfoService _selfInfoService;
        ApiTokenProvider _apiToken;

        public ProfileController(SelfInfoService selfInfoService, ApiTokenProvider apiToken)
        {
            _selfInfoService = selfInfoService;
            _apiToken = apiToken;
        }

        //[Authorize]
        public async Task<IActionResult> Rent()
        {
            if (!AuthorizeCastilCheck())
                return BadRequest("no authorize");

            var res = await _selfInfoService.GetUserRent(await Token());

            return Json(res);
        }

        ////[Authorize]
        //public async Task<IActionResult> Info()
        //{
        //    var res = await ProccesResult();
        //    return Json(res);
        //}

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> City(string city)
        {
            if (!AuthorizeCastilCheck())
                return BadRequest("no authorize");

            var user = await _selfInfoService.ChangeCity(city, Login);
            ChangeCityInCookies(city);
            return Json(user);
        }

        bool AuthorizeCastilCheck() //tempory realization
        {
            const string ASP_AUTH_TOKEN = ".AspNetCore.Cookies";
            var tokenFromHeader = HttpContext.Request.Headers.FirstOrDefault(k => k.Key == ASP_AUTH_TOKEN).Value.ToString();
            return !string.IsNullOrEmpty(tokenFromHeader);
        }

        #region helpers methods
        async Task<OutputProfileResultDto> ProccesResult()
        {
            var res = await _selfInfoService.GetUserProfile(await Token(), Login);
            res.Array = FirstProfile(res);
            return res;
        }
        List<OutputProfileDto> FirstProfile(OutputProfileResultDto rent) => rent.Array.Take(1).ToList();

        string Login => User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) ?? throw new NullReferenceException("Cookies doesn't have login.");

        string Password => User.FindFirstValue(CLAIM_PASSWORD) ?? throw new NullReferenceException("Cookies doesn't have password.");

        async Task<string> Token() => await _apiToken.GetTokenFrom(Password, Login);
        
        void ChangeCityInCookies(string city) => HttpContext.Response.Cookies.Append("city", city);
        #endregion
    }
}
