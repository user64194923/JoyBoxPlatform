namespace JoyBoxPlatform.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string BuildFolderPath { get; set; } = "";
    }
}
