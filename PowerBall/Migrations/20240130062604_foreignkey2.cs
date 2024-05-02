using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PowerBall.Migrations
{
    /// <inheritdoc />
    public partial class foreignkey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Game_Tickets_TicketsTicketID",
            //    table: "Game");

            //migrationBuilder.DropIndex(
            //    name: "IX_Game_TicketsTicketID",
            //    table: "Game");

            //migrationBuilder.DropColumn(
            //    name: "TicketsTicketID",
            //    table: "Game");

            migrationBuilder.CreateIndex(
                name: "IX_Game_TicketID",
                table: "Game",
                column: "TicketID");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Tickets_TicketID",
                table: "Game",
                column: "TicketID",
                principalTable: "Tickets",
                principalColumn: "TicketID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Game_Tickets_TicketID",
            //    table: "Game");

            //migrationBuilder.DropIndex(
            //    name: "IX_Game_TicketID",
            //    table: "Game");

            //migrationBuilder.AddColumn<int>(
            //    name: "TicketsTicketID",
            //    table: "Game",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Game_TicketsTicketID",
            //    table: "Game",
            //    column: "TicketsTicketID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Game_Tickets_TicketsTicketID",
            //    table: "Game",
            //    column: "TicketsTicketID",
            //    principalTable: "Tickets",
            //    principalColumn: "TicketID",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
