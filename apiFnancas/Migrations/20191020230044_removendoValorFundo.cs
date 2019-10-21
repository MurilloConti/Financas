using Microsoft.EntityFrameworkCore.Migrations;

namespace apiFnancas.Migrations
{
    public partial class removendoValorFundo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorTotal",
                table: "Fundos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorTotal",
                table: "Fundos",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
