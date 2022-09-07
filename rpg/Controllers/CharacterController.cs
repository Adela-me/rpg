using Microsoft.AspNetCore.Mvc;
using rpg.Models;

namespace rpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();

        [HttpGet]
        public ActionResult<Character> Get()
        {
            return Ok(knight);
        }
    }
}
