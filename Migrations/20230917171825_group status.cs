using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTracker.Migrations
{
    /// <inheritdoc />
    public partial class groupstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LecturerGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LecturerGroups");
        }
    }
}
