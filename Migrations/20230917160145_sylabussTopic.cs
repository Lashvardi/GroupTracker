using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTracker.Migrations
{
    /// <inheritdoc />
    public partial class sylabussTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentSyllabusTopicId",
                table: "LecturerGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LecturerGroups_CurrentSyllabusTopicId",
                table: "LecturerGroups",
                column: "CurrentSyllabusTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerGroups_SyllabusTopics_CurrentSyllabusTopicId",
                table: "LecturerGroups",
                column: "CurrentSyllabusTopicId",
                principalTable: "SyllabusTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerGroups_SyllabusTopics_CurrentSyllabusTopicId",
                table: "LecturerGroups");

            migrationBuilder.DropIndex(
                name: "IX_LecturerGroups_CurrentSyllabusTopicId",
                table: "LecturerGroups");

            migrationBuilder.DropColumn(
                name: "CurrentSyllabusTopicId",
                table: "LecturerGroups");
        }
    }
}
