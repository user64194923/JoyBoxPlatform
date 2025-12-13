using JoyBoxPlatform.Models;
using JoyBoxPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoyBoxPlatform.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;
        private readonly IWebHostEnvironment _env;

        public GamesController(IGameService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        // GET: api/games
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _service.GetAll();
            return Ok(games);
        }

        // GET: api/games/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var game = await _service.Get(id);
            return game == null ? NotFound() : Ok(game);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] DTOs.GameUploadDto dto)
        {
            if (dto.ZipFile == null || dto.ZipFile.Length == 0)
                return BadRequest("Invalid file");

            var gameId = Guid.NewGuid().ToString();
            var extractPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "games", gameId);
            Directory.CreateDirectory(extractPath);

            var zipPath = Path.Combine(extractPath, dto.ZipFile.FileName);
            await using (var stream = new FileStream(zipPath, FileMode.Create))
            {
                await dto.ZipFile.CopyToAsync(stream);
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
            System.IO.File.Delete(zipPath);

            var newGame = new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                BuildFolderPath = $"/games/{gameId}"
            };

            var created = await _service.Create(newGame);
            return Ok(created);
        }

    }


}
