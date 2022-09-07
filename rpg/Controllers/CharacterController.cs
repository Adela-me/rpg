using Microsoft.AspNetCore.Mvc;
using rpg.Models;

namespace rpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character {Id = 1, Name = "Sam"},
            new Character {Id = 2, Name = "Jane", RpgClass = RpgClass.Mage}
        };

        [HttpGet]
        public ActionResult<List<Character>> List()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetById(int id)
        {
            return Ok(characters.FirstOrDefault(character => character.Id == id));
        }
    }
}
