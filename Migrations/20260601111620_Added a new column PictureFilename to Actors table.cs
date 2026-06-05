using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineScope.Migrations
{
    /// <inheritdoc />
    public partial class AddedanewcolumnPictureFilenametoActorstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFilename",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "PictureFilename",
            table: "Actors");

        }
    }
}
