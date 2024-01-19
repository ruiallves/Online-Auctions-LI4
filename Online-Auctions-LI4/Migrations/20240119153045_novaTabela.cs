using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Auctions_LI4.Migrations
{
    /// <inheritdoc />
    public partial class novaTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataIncial",
                table: "Leilao",
                newName: "DataInicial");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataInicial",
                table: "Leilao",
                newName: "DataIncial");
        }
    }
}
