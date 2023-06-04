using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhythmBack.Model.Migrations
{
    /// <inheritdoc />
    public partial class DeleteFechaN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaNac",
                table: "Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaNac",
                table: "Usuario",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
