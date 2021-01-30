using Microsoft.EntityFrameworkCore.Migrations;

namespace Evo.API.Migrations
{
    public partial class _3Entidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "Nome");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuarios",
                newName: "Password");
        }
    }
}
