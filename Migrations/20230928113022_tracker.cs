using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTracker.Migrations
{
    /// <inheritdoc />
    public partial class tracker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LectionsAmount",
                table: "LecturerGroups",
                newName: "WeeksAmount");

            migrationBuilder.AddColumn<int>(
                name: "CurrentWeek",
                table: "LecturerGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentWeek",
                table: "LecturerGroups");

            migrationBuilder.RenameColumn(
                name: "WeeksAmount",
                table: "LecturerGroups",
                newName: "LectionsAmount");
        }
    }
}
