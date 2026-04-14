using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNAAnalysis.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitAppDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "GeneticResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestType",
                table: "GeneticRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "GeneticResults");

            migrationBuilder.DropColumn(
                name: "TestType",
                table: "GeneticRequests");
        }
    }
}
