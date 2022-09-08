using AutoMapper;
using Microsoft.EntityFrameworkCore;
using rpg.Data;
using rpg.Dtos.Character;
using rpg.Models;

namespace rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Create(CreateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = mapper.Map<Character>(character);
            context.Characters.Add(newCharacter);
            await context.SaveChangesAsync();
            serviceResponse.Data = await context.Characters
                .Select(character => mapper.Map<GetCharacterDto>(character))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(character => mapper.Map<GetCharacterDto>(character)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(character => character.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;

        }

        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var updatedCharacter = await context.Characters
                    .FirstOrDefaultAsync(c => character.Id == c.Id);

                updatedCharacter.Name = character.Name;
                updatedCharacter.HitPoints = character.HitPoints;
                updatedCharacter.Strength = character.Strength;
                updatedCharacter.Defense = character.Defense;
                updatedCharacter.Intelligence = character.Intelligence;
                updatedCharacter.RpgClass = character.RpgClass;

                await context.SaveChangesAsync();

                serviceResponse.Data = mapper.Map<GetCharacterDto>(updatedCharacter);
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
                var character = await context.Characters.FirstAsync(c => c.Id == id);
                context.Characters.Remove(character);
                await context.SaveChangesAsync();
                serviceResponse.Data = await context.Characters
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
    }
}
