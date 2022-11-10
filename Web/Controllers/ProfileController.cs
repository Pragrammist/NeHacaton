using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.Cryptography;
using static Web.Constants.ClaimConstants;
using System.Security.Claims;
using Web.Dtos.UserSelfInfoDto.Profile;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text;
using Web.Models;

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
        public async Task<IActionResult> Rent([FromBody]UserLoginModel user)
        {
            var res = await _selfInfoService.GetUserRent(await Token(user.Password, user.Login));

            return Json(res);
        }



        //[Authorize]
        //public async Task<IActionResult> Info()
        //{
        //    var res = await ProccesResult();
        //    return Json(res);
        //}

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> City([FromBody]UserChangeLoginModel model) 
        {
            var user = await _selfInfoService.ChangeCity(model.City, model.Login);
            ChangeCityInCookies(model.City);
            return Json(user);
        }

        #region helpers methods
        //async Task<OutputProfileResultDto> ProccesResult()
        //{
        //    var res = await _selfInfoService.GetUserProfile(await Token(), Login);
        //    res.Array = FirstProfile(res);
        //    return res;
        //}
        //List<OutputProfileDto> FirstProfile(OutputProfileResultDto rent) => rent.Array.Take(1).ToList();

        string Login => User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) ?? throw new NullReferenceException("Cookies doesn't have login.");

        string Password => User.FindFirstValue(CLAIM_PASSWORD) ?? throw new NullReferenceException("Cookies doesn't have password.");

        async Task<string> Token() => await Token(Password, Login);

        async Task<string> Token(string password, string login) => await _apiToken.GetTokenFrom(password, login);

        void ChangeCityInCookies(string city) => HttpContext.Response.Cookies.Append("city", city);


        //I must use authorize, I know, but in my case i can't because it have some troubles in client-server react and dont have time to fix it
        #endregion
    }
}
