using rpg.Models;

namespace rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        List<Character> GetAll();
        Character GetById(int id);
        List<Character> Create(Character character);
    }
}
