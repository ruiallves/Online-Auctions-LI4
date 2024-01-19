using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Auctions_LI4.Migrations
{
    /// <inheritdoc />
    public partial class testee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "semiDescricao",
                table: "Produto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "semiDescricao",
                table: "Produto");
        }
    }
}
