using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Application.Interface;
using Application.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace StormTest.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly IUserServices _userServices;
        private IConfiguration _configuration;

        public AuthController(IUserServices userServices, IConfiguration configuration)
        {
            _userServices = userServices;
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize]
        [Route("Test")]
        public IActionResult test()
        {
            return Ok();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginUser(LoginViewModel login)
        {
            var user = await _userServices.LoginUser(login);
            if (user.Data == null)
            {
                return NotFound();
            }

            var token = JwtTokenBuilder.BuildToken(user.Data, _configuration,2);

            var tokenId = await _userServices.AddToken(new AddUserTokenViewModel()
                {UserId = user.Data.Id, HashToken = token, HashTokenExTime = DateTime.Now.AddDays(2)});
            return Content(token);
        }
    }
}
