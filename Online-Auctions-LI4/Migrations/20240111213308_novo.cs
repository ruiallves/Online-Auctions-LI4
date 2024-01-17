using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Auctions_LI4.Migrations
{
    /// <inheritdoc />
    public partial class novo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Leilao");

            migrationBuilder.AddColumn<string>(
                name: "Localizacao",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Leilao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localizacao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Leilao");

            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Leilao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
