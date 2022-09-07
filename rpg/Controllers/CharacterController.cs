using Microsoft.AspNetCore.Mvc;
using rpg.Models;
using rpg.Services.CharacterService;

namespace rpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;
        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }
        [HttpGet]
        public ActionResult<List<Character>> List()
        {
            return Ok(characterService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetById(int id)
        {
            return Ok(characterService.GetById(id));
        }

        [HttpPost]
        public ActionResult<List<Character>> Create(Character character)
        {
            return Ok(characterService.Create(character));
        }
    }
}
