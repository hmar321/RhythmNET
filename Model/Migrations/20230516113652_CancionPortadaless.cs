using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhythmBack.Model.Migrations
{
    /// <inheritdoc />
    public partial class CancionPortadaless : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Portada",
                table: "Cancion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Portada",
                table: "Cancion",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
