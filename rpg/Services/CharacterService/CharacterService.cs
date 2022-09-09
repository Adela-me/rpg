using AutoMapper;
using Microsoft.EntityFrameworkCore;
using rpg.Data;
using rpg.Dtos.Character;
using rpg.Models;
using System.Security.Claims;

namespace rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(httpContextAccessor.HttpContext.User
            .FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterDto>>> Create(CreateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = mapper.Map<Character>(character);
            newCharacter.User = await context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            context.Characters.Add(newCharacter);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Characters
                .Where(c => c.User.Id == GetUserId())
                .Select(character => mapper.Map<GetCharacterDto>(character))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetByUser()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(character => character.User.Id == GetUserId())
                .ToListAsync();
            serviceResponse.Data = dbCharacters.Select(character => mapper.Map<GetCharacterDto>(character)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(character => character.Id == id && character.User.Id == GetUserId());
            serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var updatedCharacter = await context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => character.Id == c.Id);

                if (updatedCharacter.User.Id == GetUserId())
                {
                    updatedCharacter.Name = character.Name;
                    updatedCharacter.HitPoints = character.HitPoints;
                    updatedCharacter.Strength = character.Strength;
                    updatedCharacter.Defense = character.Defense;
                    updatedCharacter.Intelligence = character.Intelligence;
                    updatedCharacter.RpgClass = character.RpgClass;

                    await context.SaveChangesAsync();

                    serviceResponse.Data = mapper.Map<GetCharacterDto>(updatedCharacter);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                }
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = await context.Characters.FirstAsync(c => c.Id == id && c.User.Id == GetUserId());

                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User not found";
                    return serviceResponse;
                }

                context.Characters.Remove(character);
                await context.SaveChangesAsync();
                serviceResponse.Data = await context.Characters
                    .Where(c => c.User.Id == GetUserId())
                    .Select(c => mapper.Map<GetCharacterDto>(c))
                    .ToListAsync();

            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto characterSkill)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == characterSkill.CharacterId &&
                    c.User.Id == GetUserId());

                if (character == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found";
                    return serviceResponse;
                }

                var skill = await context.Skills
                    .FirstOrDefaultAsync(s => s.Id == characterSkill.SkillId);

                if (skill == null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Skill not found";
                    return serviceResponse;
                }

                character.Skills.Add(skill);
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
