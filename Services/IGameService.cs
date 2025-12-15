using JoyBoxPlatform.Models;

namespace JoyBoxPlatform.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAll();
        Task<Game?> Get(int id);
        Task<Game> Create(Game game);

        Task Update(Game game);
        Task Delete(int id);
    }
}
