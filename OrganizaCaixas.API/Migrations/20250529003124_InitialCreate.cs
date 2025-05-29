using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrganizaCaixas.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caixas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCaixa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Altura = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Largura = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Comprimento = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caixas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Caixas",
                columns: new[] { "Id", "Altura", "Comprimento", "Largura", "NomeCaixa" },
                values: new object[,]
                {
                    { 1, 30m, 80m, 40m, "Caixa 1" },
                    { 2, 80m, 40m, 50m, "Caixa 2" },
                    { 3, 50m, 60m, 80m, "Caixa 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caixas");
        }
    }
}
