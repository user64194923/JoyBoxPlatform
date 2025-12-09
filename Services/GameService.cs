using JoyBoxPlatform.Data;
using JoyBoxPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace JoyBoxPlatform.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
        }

        /*
        public GameService(AppDbContext context)
        {
            _context = context;

            // Inicializar con algunos juegos por defecto
            if (!_context.Games.Any())
            {
                _context.Games.AddRange(
                    new Game { Title = "Super Fun Game", Description = "A fun game to test." },
                    new Game { Title = "Adventure Quest", Description = "An adventurous journey." }
                );
                _context.SaveChanges();
            }
        }
        */
        public async Task<Game> Create(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<List<Game>> GetAll()
            => await _context.Games.ToListAsync();

        public async Task<Game?> Get(int id)
            => await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
    }
}
