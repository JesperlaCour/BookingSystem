using Microsoft.EntityFrameworkCore.Migrations;

namespace EventAPI.Migrations
{
    public partial class DeliveryCommentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Delivering",
                table: "Events",
                newName: "Delivery");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryComments",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryComments",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Delivery",
                table: "Events",
                newName: "Delivering");
        }
    }
}
