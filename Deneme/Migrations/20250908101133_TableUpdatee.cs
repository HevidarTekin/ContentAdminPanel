using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deneme.Migrations
{
    /// <inheritdoc />
    public partial class TableUpdatee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Projects");
        }
    }
}
