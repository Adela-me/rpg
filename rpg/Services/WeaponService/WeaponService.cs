using AutoMapper;
using Microsoft.EntityFrameworkCore;
using rpg.Data;
using rpg.Dtos.Character;
using rpg.Dtos.Weapon;
using rpg.Models;
using System.Security.Claims;

namespace rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<GetCharacterDto>> Create(CreateWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                    c.User.Id == int.Parse(httpContextAccessor.HttpContext.User
                    .FindFirstValue(ClaimTypes.NameIdentifier)));
                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                    return serviceResponse;
                }

                Weapon weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Demage = newWeapon.Demage,
                    Character = character
                };

                context.Weapons.Add(weapon);
                await context.SaveChangesAsync();
                serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }
    }
}
