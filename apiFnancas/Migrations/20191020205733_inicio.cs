using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace apiFnancas.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Indices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Valor = table.Column<decimal>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    DtAlteracao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Nome = table.Column<string>(nullable: true),
                    CPF = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carteiras",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Nome = table.Column<string>(nullable: true),
                    ProprietarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteiras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carteiras_Pessoas_ProprietarioId",
                        column: x => x.ProprietarioId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fundos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Nome = table.Column<string>(nullable: true),
                    CNPJ = table.Column<long>(nullable: false),
                    TxPerformance = table.Column<decimal>(nullable: false),
                    TxAdministracao = table.Column<decimal>(nullable: false),
                    TxImpRenda = table.Column<decimal>(nullable: false),
                    TxResgate = table.Column<decimal>(nullable: false),
                    QtdCotas = table.Column<decimal>(nullable: false),
                    ValorCota = table.Column<decimal>(nullable: false),
                    ValorTotal = table.Column<decimal>(nullable: false),
                    PercIndice = table.Column<decimal>(nullable: false),
                    BenchmarkId = table.Column<int>(nullable: true),
                    ANBIMA = table.Column<int>(nullable: false),
                    ISIN = table.Column<string>(nullable: true),
                    CarteiraId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fundos_Indices_BenchmarkId",
                        column: x => x.BenchmarkId,
                        principalTable: "Indices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fundos_Carteiras_CarteiraId",
                        column: x => x.CarteiraId,
                        principalTable: "Carteiras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rendimentos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    FundoId = table.Column<int>(nullable: true),
                    SaldoAnterior = table.Column<decimal>(nullable: false),
                    SaldoFinal = table.Column<decimal>(nullable: false),
                    LuroApurado = table.Column<decimal>(nullable: false),
                    PercLucro = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rendimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rendimentos_Fundos_FundoId",
                        column: x => x.FundoId,
                        principalTable: "Fundos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_ProprietarioId",
                table: "Carteiras",
                column: "ProprietarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fundos_BenchmarkId",
                table: "Fundos",
                column: "BenchmarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Fundos_CarteiraId",
                table: "Fundos",
                column: "CarteiraId");

            migrationBuilder.CreateIndex(
                name: "IX_Rendimentos_FundoId",
                table: "Rendimentos",
                column: "FundoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rendimentos");

            migrationBuilder.DropTable(
                name: "Fundos");

            migrationBuilder.DropTable(
                name: "Indices");

            migrationBuilder.DropTable(
                name: "Carteiras");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
