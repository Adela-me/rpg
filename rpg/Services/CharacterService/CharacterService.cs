using AutoMapper;
using rpg.Dtos.Character;
using rpg.Models;

namespace rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character {Id = 1, Name = "Sam"},
            new Character {Id = 2, Name = "Jane", RpgClass = RpgClass.Mage}
        };

        private readonly IMapper mapper;
        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> Create(CreateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            characters.Add(mapper.Map<Character>(character));
            serviceResponse.Data = characters.Select(character => mapper.Map<GetCharacterDto>(character)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            return new ServiceResponse<List<GetCharacterDto>> { Data = characters.Select(character => mapper.Map<GetCharacterDto>(character)).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(character => character.Id == id);
            serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
            return serviceResponse;

        }
    }
}
