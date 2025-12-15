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

        public async Task Update(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null) return;

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }


}
