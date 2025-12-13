namespace JoyBoxPlatform.DTOs
{
    public class GameUploadDto
    {
        public IFormFile ZipFile { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
