using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerBall.Migrations
{
    /// <inheritdoc />
    public partial class makedrawId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Draw",
                newName: "DrawId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrawId",
                table: "Draw",
                newName: "Id");
        }
    }
}
