using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using rpg.Dtos.Character;
using rpg.Dtos.Weapon;
using rpg.Models;
using rpg.Services.WeaponService;

namespace rpg.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            this.weaponService = weaponService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Create(CreateWeaponDto newWeapon)
        {
            return Ok(await weaponService.Create(newWeapon));
        }
    }
}
