using Microsoft.EntityFrameworkCore.Migrations;

namespace DecaBlog.Data.Migrations
{
    public partial class addActionPerformerToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActionPerformedBy",
                table: "Notifications",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionPerformedBy",
                table: "Notifications");
        }
    }
}
