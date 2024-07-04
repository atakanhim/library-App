using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libraryApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class hk31 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Notes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
