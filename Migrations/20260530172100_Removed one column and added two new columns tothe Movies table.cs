using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CineScope.Migrations
{
    /// <inheritdoc />
    public partial class RemovedonecolumnandaddedtwonewcolumnstotheMoviestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterUrl",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "PosterFilename",
                table: "Movies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PosterFilenameWide",
                table: "Movies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterFilename",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "PosterFilenameWide",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "PosterUrl",
                table: "Movies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
