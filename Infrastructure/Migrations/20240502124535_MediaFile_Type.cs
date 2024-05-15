using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MediaFile_Type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "MediaFile");

            migrationBuilder.AddColumn<int>(
                name: "MediaFileType",
                table: "MediaFile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaFileType",
                table: "MediaFile");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "MediaFile",
                type: "longtext",
                nullable: false);
        }
    }
}
