using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DNAAnalysis.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixReminderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Reminders",
                newName: "StartTime");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "Reminders",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reminders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Reminders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reminders");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Reminders",
                newName: "Time");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Reminders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
