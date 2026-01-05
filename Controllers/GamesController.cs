using JoyBoxPlatform.Models;
using JoyBoxPlatform.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;


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

        // POST: api/games/upload
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] DTOs.GameUploadDto dto)
        {
            if (dto.ZipFile == null || dto.ZipFile.Length == 0)
                return BadRequest("Invalid file");

            var gameId = Guid.NewGuid().ToString();
            var extractPath = Path.Combine(_env.WebRootPath!, "games", gameId);
            Directory.CreateDirectory(extractPath);

            var zipPath = Path.Combine(extractPath, dto.ZipFile.FileName);

            // Save ZIP
            await using (var stream = new FileStream(zipPath, FileMode.Create))
            {
                await dto.ZipFile.CopyToAsync(stream);
            }

            bool hasIndex = false;

            // OPEN ZIP
            using (var archive = ZipFile.OpenRead(zipPath))
            {
                foreach (var entry in archive.Entries)
                {
                    var fullPath = Path.GetFullPath(
                        Path.Combine(extractPath, entry.FullName)
                    );

                    // ZIP SLIP PROTECTION
                    if (!fullPath.StartsWith(extractPath))
                        return BadRequest("Invalid ZIP structure");

                    if (entry.FullName.EndsWith("index.html", StringComparison.OrdinalIgnoreCase))
                        hasIndex = true;
                }

                if (!hasIndex)
                    return BadRequest("ZIP must contain index.html");

                // Extract safely
                archive.ExtractToDirectory(extractPath, overwriteFiles: true);
            } // <-- ZIP IS CLOSED HERE 🔑

            // NOW it's safe to delete
            System.IO.File.Delete(zipPath);

            // Auto thumbnail detection
            string thumbnailPath = "/images/default-game.png";
            var allowedNames = new[] { "icon.png", "thumbnail.png", "screenshot.png" };

            foreach (var name in allowedNames)
            {
                var found = Directory
                    .GetFiles(extractPath, name, SearchOption.AllDirectories)
                    .FirstOrDefault();

                if (found != null)
                {
                    thumbnailPath = found
                        .Replace(_env.WebRootPath!, "")
                        .Replace("\\", "/");
                    break;
                }
            }

            var newGame = new Game
            {
                Title = dto.Title,
                Description = dto.Description,
                BuildFolderPath = $"/games/{gameId}",
                ThumbnailPath = thumbnailPath
            };

            var created = await _service.Create(newGame);
            return Ok(created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game updated)
        {
            var game = await _service.Get(id);
            if (game == null)
                return NotFound();

            game.Title = updated.Title;
            game.Description = updated.Description;

            await _service.Update(game);
            return Ok(game);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _service.Get(id);
            if (game == null)
                return NotFound();

            // delete game folder
            if (!string.IsNullOrEmpty(game.BuildFolderPath))
            {
                var path = Path.Combine(_env.WebRootPath!, game.BuildFolderPath.TrimStart('/'));
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }

            await _service.Delete(id);
            return Ok();
        }


    }


}
