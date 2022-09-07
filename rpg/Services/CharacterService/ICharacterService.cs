using rpg.Models;

namespace rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<List<Character>> GetAll();
        Task<Character> GetById(int id);
        Task<List<Character>> Create(Character character);
    }
}
