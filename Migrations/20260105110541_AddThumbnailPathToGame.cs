using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyBoxPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailPathToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPath",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailPath",
                table: "Games");
        }
    }
}
