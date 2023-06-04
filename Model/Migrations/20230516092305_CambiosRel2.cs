using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhythmBack.Model.Migrations
{
    /// <inheritdoc />
    public partial class CambiosRel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_Lista_ListaId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_ListaId",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "ListaId",
                table: "Usuario");

            migrationBuilder.CreateTable(
                name: "ListaUsuario",
                columns: table => new
                {
                    ListasId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaUsuario", x => new { x.ListasId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_ListaUsuario_Lista_ListasId",
                        column: x => x.ListasId,
                        principalTable: "Lista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListaUsuario_Usuario_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ListaUsuario_UsuariosId",
                table: "ListaUsuario",
                column: "UsuariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListaUsuario");

            migrationBuilder.AddColumn<int>(
                name: "ListaId",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ListaId",
                table: "Usuario",
                column: "ListaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_Lista_ListaId",
                table: "Usuario",
                column: "ListaId",
                principalTable: "Lista",
                principalColumn: "Id");
        }
    }
}
