using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libraryApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5131 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Books_BookId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Books_BookId",
                table: "Files",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Books_BookId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Books_BookId",
                table: "Files",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
