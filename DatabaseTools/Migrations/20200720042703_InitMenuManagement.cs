using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations
{
    public partial class InitMenuManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MENUS",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    ORDER = table.Column<int>(nullable: false),
                    NAME = table.Column<string>(maxLength: 50, nullable: false),
                    DISPLAY_NAME = table.Column<string>(maxLength: 255, nullable: false),
                    ICON = table.Column<string>(maxLength: 255, nullable: true),
                    HIERARCHY_CODE = table.Column<string>(nullable: false),
                    CONTROLLER = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENUS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MENU_ROLE",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    MENU_ID = table.Column<string>(nullable: true),
                    ROLE_ID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MENU_ROLE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MENU_ROLE_MENUS_MENU_ID",
                        column: x => x.MENU_ID,
                        principalTable: "MENUS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MENU_ROLE_ROLES_ROLE_ID",
                        column: x => x.ROLE_ID,
                        principalTable: "ROLES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MENU_ROLE_MENU_ID",
                table: "MENU_ROLE",
                column: "MENU_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MENU_ROLE_ROLE_ID",
                table: "MENU_ROLE",
                column: "ROLE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MENU_ROLE");

            migrationBuilder.DropTable(
                name: "MENUS");
        }
    }
}
