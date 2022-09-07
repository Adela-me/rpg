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
        public async Task<List<Character>> Create(Character character)
        {
            characters.Add(character);
            return characters;
        }

        public async Task<List<Character>> GetAll()
        {
            return characters;
        }

        public async Task<Character> GetById(int id)
        {
            return characters.FirstOrDefault(character => character.Id == id);
        }
    }
}
