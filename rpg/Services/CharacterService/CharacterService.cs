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
        public async Task<ServiceResponse<List<Character>>> Create(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            characters.Add(character);
            serviceResponse.Data = characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAll()
        {
            return new ServiceResponse<List<Character>> { Data = characters };
        }

        public async Task<ServiceResponse<Character>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            var character = characters.FirstOrDefault(character => character.Id == id);
            serviceResponse.Data = character;
            return serviceResponse;

        }
    }
}
