using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCompra.API.Migrations
{
    public partial class UpdateSolicitaoCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondicaoPagamento",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "NomeFornecedor",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "UsuarioSolicitante",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "TotalGeral",
                table: "SolicitacaoCompra");

            migrationBuilder.AddColumn<Guid>(
                name: "CondicaoPagamentoId",
                table: "SolicitacaoCompra",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdCondicaoPagamento",
                table: "SolicitacaoCompra",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdNomeFornecedor",
                table: "SolicitacaoCompra",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuarioSolicitante",
                table: "SolicitacaoCompra",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "NomeFornecedorId",
                table: "SolicitacaoCompra",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioSolicitanteId",
                table: "SolicitacaoCompra",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CondicaoPagamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Valor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicaoPagamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NomeFornecedor",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomeFornecedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioSolicitante",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioSolicitante", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CondicaoPagamento",
                columns: new[] { "Id", "Valor" },
                values: new object[,]
                {
                    { new Guid("249dc498-36c9-424f-ba23-a5ee8f3ad389"), 30 },
                    { new Guid("dc199f14-9182-40c3-ba96-41b742c7f448"), 30 }
                });

            migrationBuilder.InsertData(
                table: "NomeFornecedor",
                column: "Id",
                values: new object[]
                {
                    new Guid("0511ce33-78e7-472d-a297-2d0b7438c2bb"),
                    new Guid("9c9a03dd-1704-4856-9e14-477ebf7a6c38")
                });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "Categoria", "Descricao", "Nome", "Preco", "Situacao" },
                values: new object[] { new Guid("ec195a24-f7c4-4656-b7a4-044e2b16fa46"), 1, "Descricao01", "Produto01", 100m, 1 });

            migrationBuilder.InsertData(
                table: "SolicitacaoCompra",
                columns: new[] { "Id", "CondicaoPagamentoId", "Data", "IdCondicaoPagamento", "IdNomeFornecedor", "IdUsuarioSolicitante", "NomeFornecedorId", "Situacao", "UsuarioSolicitanteId" },
                values: new object[] { new Guid("36152e5e-dbbb-4830-b8ea-edad75407a69"), null, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), new Guid("dc199f14-9182-40c3-ba96-41b742c7f448"), new Guid("9c9a03dd-1704-4856-9e14-477ebf7a6c38"), new Guid("c7535326-d2c5-4394-9f49-97d7b868088d"), null, 1, null });

            migrationBuilder.InsertData(
                table: "UsuarioSolicitante",
                column: "Id",
                values: new object[]
                {
                    new Guid("2850c53d-3751-4358-bbba-0a0a783427e0"),
                    new Guid("c7535326-d2c5-4394-9f49-97d7b868088d")
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCompra_CondicaoPagamentoId",
                table: "SolicitacaoCompra",
                column: "CondicaoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCompra_NomeFornecedorId",
                table: "SolicitacaoCompra",
                column: "NomeFornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCompra_UsuarioSolicitanteId",
                table: "SolicitacaoCompra",
                column: "UsuarioSolicitanteId");

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCompra_CondicaoPagamento_CondicaoPagamentoId",
                table: "SolicitacaoCompra",
                column: "CondicaoPagamentoId",
                principalTable: "CondicaoPagamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCompra_NomeFornecedor_NomeFornecedorId",
                table: "SolicitacaoCompra",
                column: "NomeFornecedorId",
                principalTable: "NomeFornecedor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCompra_UsuarioSolicitante_UsuarioSolicitanteId",
                table: "SolicitacaoCompra",
                column: "UsuarioSolicitanteId",
                principalTable: "UsuarioSolicitante",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCompra_CondicaoPagamento_CondicaoPagamentoId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCompra_NomeFornecedor_NomeFornecedorId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCompra_UsuarioSolicitante_UsuarioSolicitanteId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropTable(
                name: "CondicaoPagamento");

            migrationBuilder.DropTable(
                name: "NomeFornecedor");

            migrationBuilder.DropTable(
                name: "UsuarioSolicitante");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCompra_CondicaoPagamentoId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCompra_NomeFornecedorId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCompra_UsuarioSolicitanteId",
                table: "SolicitacaoCompra");

            migrationBuilder.DeleteData(
                table: "Produto",
                keyColumn: "Id",
                keyValue: new Guid("ec195a24-f7c4-4656-b7a4-044e2b16fa46"));

            migrationBuilder.DeleteData(
                table: "SolicitacaoCompra",
                keyColumn: "Id",
                keyValue: new Guid("36152e5e-dbbb-4830-b8ea-edad75407a69"));

            migrationBuilder.DropColumn(
                name: "CondicaoPagamentoId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "IdCondicaoPagamento",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "IdNomeFornecedor",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "IdUsuarioSolicitante",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "NomeFornecedorId",
                table: "SolicitacaoCompra");

            migrationBuilder.DropColumn(
                name: "UsuarioSolicitanteId",
                table: "SolicitacaoCompra");

            migrationBuilder.AddColumn<int>(
                name: "CondicaoPagamento",
                table: "SolicitacaoCompra",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeFornecedor",
                table: "SolicitacaoCompra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioSolicitante",
                table: "SolicitacaoCompra",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalGeral",
                table: "SolicitacaoCompra",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
