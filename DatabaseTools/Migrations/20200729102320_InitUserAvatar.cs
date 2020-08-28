using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations
{
    public partial class InitUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PROFILE_IMAGE",
                table: "USERS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROFILE_IMAGE",
                table: "USERS");
        }
    }
}
