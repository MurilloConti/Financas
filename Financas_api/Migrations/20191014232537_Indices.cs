using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Financas_api.Migrations
{
    public partial class Indices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcCDI",
                table: "Fundos");

            migrationBuilder.AddColumn<int>(
                name: "BenchmarkId",
                table: "Fundos",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Indices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indices", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fundos_BenchmarkId",
                table: "Fundos",
                column: "BenchmarkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fundos_Indices_BenchmarkId",
                table: "Fundos",
                column: "BenchmarkId",
                principalTable: "Indices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fundos_Indices_BenchmarkId",
                table: "Fundos");

            migrationBuilder.DropTable(
                name: "Indices");

            migrationBuilder.DropIndex(
                name: "IX_Fundos_BenchmarkId",
                table: "Fundos");

            migrationBuilder.DropColumn(
                name: "BenchmarkId",
                table: "Fundos");

            migrationBuilder.AddColumn<decimal>(
                name: "ProcCDI",
                table: "Fundos",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
