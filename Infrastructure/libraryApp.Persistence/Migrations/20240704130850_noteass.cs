using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libraryApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class noteass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Privacy",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Privacy",
                table: "Notes");
        }
    }
}
