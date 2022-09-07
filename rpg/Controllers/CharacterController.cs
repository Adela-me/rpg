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
        public async Task<ActionResult<ServiceResponse<List<Character>>>> List()
        {
            return Ok(await characterService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetById(int id)
        {
            return Ok(await characterService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> Create(Character character)
        {
            return Ok(await characterService.Create(character));
        }
    }
}
