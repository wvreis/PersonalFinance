using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinance.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class deleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTypes_TransactionTypeGroups_TransactionTypeGroup~",
                table: "TransactionTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTypes_TransactionTypeGroups_TransactionTypeGroup~",
                table: "TransactionTypes",
                column: "TransactionTypeGroupId",
                principalTable: "TransactionTypeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTypes_TransactionTypeGroups_TransactionTypeGroup~",
                table: "TransactionTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTypes_TransactionTypeGroups_TransactionTypeGroup~",
                table: "TransactionTypes",
                column: "TransactionTypeGroupId",
                principalTable: "TransactionTypeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
