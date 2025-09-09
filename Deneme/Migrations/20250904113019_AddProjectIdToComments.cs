using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deneme.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectIdToComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "Comments",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_BlogId",
                table: "Comments",
                newName: "IX_Comments_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Projects_ProjectId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Comments",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ProjectId",
                table: "Comments",
                newName: "IX_Comments_BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Blogs_BlogId",
                table: "Comments",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "BlogId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
