using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Auctions_LI4.Migrations
{
    /// <inheritdoc />
    public partial class newsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HistoricoDeLances_ID",
                table: "Leilao");

            migrationBuilder.DropColumn(
                name: "Licitacao_ID",
                table: "Leilao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HistoricoDeLances_ID",
                table: "Leilao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Licitacao_ID",
                table: "Leilao",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
