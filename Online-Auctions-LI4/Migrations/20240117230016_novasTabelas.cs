using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Auctions_LI4.Migrations
{
    /// <inheritdoc />
    public partial class novasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Quantia",
                table: "Leilao",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Leilao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Leilao");

            migrationBuilder.AlterColumn<int>(
                name: "Quantia",
                table: "Leilao",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
