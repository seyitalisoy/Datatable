using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto dto)
        {
            var userLoginResult = _authService.Login(dto);
            if (!userLoginResult.Success)
            {
                return BadRequest(userLoginResult.Message);
            }

            var result = _authService.CreateAccessToken(userLoginResult.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest("Token üretilirken hata oluştu");
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto dto)
        {
            var userExists = _authService.UserExists(dto.Email);

            if (userExists.Success)
            {
                return Ok("Kullanıcı zaten mevcut, lütfen giriş yapınız");
            }

            var registerResult = _authService.Register(dto);

            if (!registerResult.Success)
            {
                return BadRequest("Kullanıcı eklenirken hata oluştu");
            }

            var result = _authService.CreateAccessToken(registerResult.Data);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest("Token üretilirken hata oluştu");

        }
    }
}
