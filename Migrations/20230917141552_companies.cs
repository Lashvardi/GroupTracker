using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTracker.Migrations
{
    /// <inheritdoc />
    public partial class companies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsOnline",
                table: "Groups",
                newName: "Grade");

            migrationBuilder.AddColumn<bool>(
                name: "IsAlternate",
                table: "LectureSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                table: "LectureSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Companies",
                table: "Lecturers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlternateWeeks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    LectureSessionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternateWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlternateWeeks_LectureSessions_LectureSessionId",
                        column: x => x.LectureSessionId,
                        principalTable: "LectureSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SyllabusTopics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    LecturerGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SyllabusTopics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SyllabusTopics_Groups_LecturerGroupId",
                        column: x => x.LecturerGroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternateWeeks_LectureSessionId",
                table: "AlternateWeeks",
                column: "LectureSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SyllabusTopics_LecturerGroupId",
                table: "SyllabusTopics",
                column: "LecturerGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternateWeeks");

            migrationBuilder.DropTable(
                name: "SyllabusTopics");

            migrationBuilder.DropColumn(
                name: "IsAlternate",
                table: "LectureSessions");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                table: "LectureSessions");

            migrationBuilder.DropColumn(
                name: "Companies",
                table: "Lecturers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Groups");

            migrationBuilder.RenameColumn(
                name: "Grade",
                table: "Groups",
                newName: "IsOnline");
        }
    }
}
