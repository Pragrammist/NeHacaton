﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Services;
using Web.Cryptographer;
using static Web.Constants.ClaimConstants;
using System.Security.Claims;
using Microsoft.AspNetCore.Cors;

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
            var res = await _selfInfoService.GetUserRentSelfInfo(await Token());

            return Json(res);
        }
        [Authorize]
        public async Task<IActionResult> Info()
        {
            var res = await _selfInfoService.GetUserProfileSelfInfo(await Token());

            return Json(res);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> City(string city)
        {
            var user = await _selfInfoService.ChangeCity(city, Login);
            return Json(user);
        }

        string Login => User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) ?? throw new NullReferenceException("Cookies doesn't have login.");

        async Task<string> Token()
        {
            var encryptPassword = User.FindFirstValue(CLAIM_PASSWORD) ?? throw new NullReferenceException("Cookies doesn't have password.");
            var login = Login;
            var password = _cryptographer.Decrypt(encryptPassword);
            var token = await _apiToken.GetToken(password, login);
            return token;
        }
    }
}
