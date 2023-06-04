using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RhythmBack.Model.Migrations
{
    /// <inheritdoc />
    public partial class CambiosRel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumUsuario_Usuario_UsuariosId",
                table: "AlbumUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistaUsuario_Usuario_UsuariosId",
                table: "ArtistaUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaUsuario_Usuario_UsuariosId",
                table: "ListaUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "ListaUsuario",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ListaUsuario_UsuariosId",
                table: "ListaUsuario",
                newName: "IX_ListaUsuario_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "ArtistaUsuario",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistaUsuario_UsuariosId",
                table: "ArtistaUsuario",
                newName: "IX_ArtistaUsuario_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "AlbumUsuario",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumUsuario_UsuariosId",
                table: "AlbumUsuario",
                newName: "IX_AlbumUsuario_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumUsuario_Usuario_UsuarioId",
                table: "AlbumUsuario",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistaUsuario_Usuario_UsuarioId",
                table: "ArtistaUsuario",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaUsuario_Usuario_UsuarioId",
                table: "ListaUsuario",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumUsuario_Usuario_UsuarioId",
                table: "AlbumUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_ArtistaUsuario_Usuario_UsuarioId",
                table: "ArtistaUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_ListaUsuario_Usuario_UsuarioId",
                table: "ListaUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ListaUsuario",
                newName: "UsuariosId");

            migrationBuilder.RenameIndex(
                name: "IX_ListaUsuario_UsuarioId",
                table: "ListaUsuario",
                newName: "IX_ListaUsuario_UsuariosId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "ArtistaUsuario",
                newName: "UsuariosId");

            migrationBuilder.RenameIndex(
                name: "IX_ArtistaUsuario_UsuarioId",
                table: "ArtistaUsuario",
                newName: "IX_ArtistaUsuario_UsuariosId");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "AlbumUsuario",
                newName: "UsuariosId");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumUsuario_UsuarioId",
                table: "AlbumUsuario",
                newName: "IX_AlbumUsuario_UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumUsuario_Usuario_UsuariosId",
                table: "AlbumUsuario",
                column: "UsuariosId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ArtistaUsuario_Usuario_UsuariosId",
                table: "ArtistaUsuario",
                column: "UsuariosId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListaUsuario_Usuario_UsuariosId",
                table: "ListaUsuario",
                column: "UsuariosId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
