using Microsoft.AspNetCore.Mvc;
using rpg.Data;
using rpg.Dtos.User;
using rpg.Models;

namespace rpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            this.authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
        {
            var response = await authRepository.Register(
                new User { Username = request.Username }, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return response;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request)
        {
            var response = await authRepository.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return response;
        }
    }
}
