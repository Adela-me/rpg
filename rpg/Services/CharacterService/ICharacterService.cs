using rpg.Dtos.Character;
using rpg.Models;

namespace rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetByUser(int userId);
        Task<ServiceResponse<GetCharacterDto>> GetById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> Create(CreateCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto character);
        Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id);
    }
}
