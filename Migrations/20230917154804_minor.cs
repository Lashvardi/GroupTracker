using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTracker.Migrations
{
    /// <inheritdoc />
    public partial class minor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupLectureSessions_Groups_GroupId",
                table: "GroupLectureSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Lecturers_LecturerId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllabusTopics_Groups_LecturerGroupId",
                table: "SyllabusTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Groups",
                table: "Groups");

            migrationBuilder.RenameTable(
                name: "Groups",
                newName: "LecturerGroups");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_LecturerId",
                table: "LecturerGroups",
                newName: "IX_LecturerGroups_LecturerId");

            migrationBuilder.AlterColumn<string>(
                name: "WeekNumber",
                table: "AlternateWeeks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LecturerGroups",
                table: "LecturerGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupLectureSessions_LecturerGroups_GroupId",
                table: "GroupLectureSessions",
                column: "GroupId",
                principalTable: "LecturerGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerGroups_Lecturers_LecturerId",
                table: "LecturerGroups",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyllabusTopics_LecturerGroups_LecturerGroupId",
                table: "SyllabusTopics",
                column: "LecturerGroupId",
                principalTable: "LecturerGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupLectureSessions_LecturerGroups_GroupId",
                table: "GroupLectureSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerGroups_Lecturers_LecturerId",
                table: "LecturerGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_SyllabusTopics_LecturerGroups_LecturerGroupId",
                table: "SyllabusTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LecturerGroups",
                table: "LecturerGroups");

            migrationBuilder.RenameTable(
                name: "LecturerGroups",
                newName: "Groups");

            migrationBuilder.RenameIndex(
                name: "IX_LecturerGroups_LecturerId",
                table: "Groups",
                newName: "IX_Groups_LecturerId");

            migrationBuilder.AlterColumn<int>(
                name: "WeekNumber",
                table: "AlternateWeeks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groups",
                table: "Groups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupLectureSessions_Groups_GroupId",
                table: "GroupLectureSessions",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Lecturers_LecturerId",
                table: "Groups",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SyllabusTopics_Groups_LecturerGroupId",
                table: "SyllabusTopics",
                column: "LecturerGroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
