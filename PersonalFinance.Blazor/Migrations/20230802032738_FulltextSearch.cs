using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace PersonalFinance.Blazor.Migrations
{
    /// <inheritdoc />
    public partial class FulltextSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Transactions",
                type: "tsvector",
                nullable: true)
                .Annotation("Npgsql:TsVectorConfig", "portuguese")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Description" });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SearchVector",
                table: "Transactions",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_SearchVector",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Transactions");
        }
    }
}
