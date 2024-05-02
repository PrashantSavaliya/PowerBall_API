using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerBall.Migrations
{
    /// <inheritdoc />
    public partial class makedrawId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrawId",
                table: "Draw");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrawId",
                table: "Draw",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
