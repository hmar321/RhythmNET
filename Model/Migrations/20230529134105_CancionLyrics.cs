using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhythmBack.Model.Migrations
{
    /// <inheritdoc />
    public partial class CancionLyrics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Lyrics",
                table: "Cancion",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lyrics",
                table: "Cancion");
        }
    }
}
