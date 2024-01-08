using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Auctions_LI4.Migrations
{
    /// <inheritdoc />
    public partial class maistabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AreaUtil",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CertificadoEnergetico",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Condicao",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipologia",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaUtil",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CertificadoEnergetico",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Condicao",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Tipologia",
                table: "Produto");
        }
    }
}
