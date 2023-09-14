using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalFinance.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class TransactionTypeGroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeGroupId",
                table: "TransactionTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransactionTypeGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypeGroups", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTypes_TransactionTypeGroupId",
                table: "TransactionTypes",
                column: "TransactionTypeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionTypes_TransactionTypeGroups_TransactionTypeGroup~",
                table: "TransactionTypes",
                column: "TransactionTypeGroupId",
                principalTable: "TransactionTypeGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionTypes_TransactionTypeGroups_TransactionTypeGroup~",
                table: "TransactionTypes");

            migrationBuilder.DropTable(
                name: "TransactionTypeGroups");

            migrationBuilder.DropIndex(
                name: "IX_TransactionTypes_TransactionTypeGroupId",
                table: "TransactionTypes");

            migrationBuilder.DropColumn(
                name: "TransactionTypeGroupId",
                table: "TransactionTypes");
        }
    }
}
