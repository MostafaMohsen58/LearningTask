using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditedColumnInMaterialTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "LessonMaterials");

            migrationBuilder.AddColumn<string>(
                name: "ContentUrl",
                table: "LessonMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentUrl",
                table: "LessonMaterials");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "LessonMaterials",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
