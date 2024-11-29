using Microsoft.AspNetCore.Mvc;
using Programowanie_aplikacji_internetowych.domain.Dtos.Users;
using Programowanie_aplikacji_internetowych.domain.Exceptions;
using Programowanie_aplikacji_internetowych.domain.Interfaces.Services;

namespace Programowanie_aplikacji_internetowych.webapi.Controllers
{
    [ApiController]
    [Route("api/User")]
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
            return Ok(token);
        }
    }
}
