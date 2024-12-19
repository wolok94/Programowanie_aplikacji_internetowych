using Microsoft.AspNetCore.Mvc;
using Programowanie_aplikacji_internetowych.domain.Dtos.Users;
using Programowanie_aplikacji_internetowych.domain.Exceptions;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

namespace Programowanie_aplikacji_internetowych.webapi.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelException("Model rejestracji jest niezgodny");
            }
            await _userService.Register(registerUserDto);
            return Ok();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (!ModelState.IsValid)
            {
                throw new InvalidModelException("Model rejestracji jest niezgodny");
            }
            var token = await _userService.Login(loginUserDto);

            Response.Headers.Add("X-Access-Token", token.AccessToken);
            Response.Headers.Add("X-Refresh-Token", token.RefreshToken.Token);
            Response.Headers.Add("X-Access-Token-ExpiresAt", token.AccessTokenExpiresAt.ToString());
            Response.Headers.Add("X-Refresh-Token-ExpiresAt", token.RefreshToken.ExpiryDate.ToString());

            return Ok(token);
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            string accessToken = "";
            string refreshToken = "";

            if (Request.Headers.TryGetValue("Access-Token", out var accessTokenHeader))
            {
                accessToken = accessTokenHeader.ToString();
            }
            if (Request.Headers.TryGetValue("Refresh-Token", out var refreshTokenHeader))
            {
                refreshToken = refreshTokenHeader.ToString();
            }

            var token = await _userService.RefreshToken(accessToken, refreshToken);

            Response.Headers.Add("X-Access-Token", token.AccessToken);
            Response.Headers.Add("X-Refresh-Token", token.RefreshToken.Token);
            Response.Headers.Add("X-Access-Token-ExpiresAt", token.AccessTokenExpiresAt.ToString());
            Response.Headers.Add("X-Refresh-Token-ExpiresAt", token.RefreshToken.ExpiryDate.ToString());

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }
    }
}

