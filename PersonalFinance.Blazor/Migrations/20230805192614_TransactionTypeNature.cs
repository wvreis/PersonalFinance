using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinance.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTypeNature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nature",
                table: "TransactionTypes",
                type: "integer",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nature",
                table: "TransactionTypes");
        }
    }
}
