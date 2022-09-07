using rpg.Models;

namespace rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<Character>>> GetAll();
        Task<ServiceResponse<Character>> GetById(int id);
        Task<ServiceResponse<List<Character>>> Create(Character character);
    }
}
