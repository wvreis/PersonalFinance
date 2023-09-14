using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinance.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class TransactionNatureInTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nature",
                table: "Transactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nature",
                table: "Transactions");
        }
    }
}
