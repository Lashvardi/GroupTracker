using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTracker.Migrations
{
    /// <inheritdoc />
    public partial class totallections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectionsAmount",
                table: "LecturerGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "WeekNumber",
                table: "AlternateWeeks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LectionsAmount",
                table: "LecturerGroups");

            migrationBuilder.AlterColumn<string>(
                name: "WeekNumber",
                table: "AlternateWeeks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
