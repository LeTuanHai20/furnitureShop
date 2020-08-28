using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations
{
    public partial class InitPageSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MAIL_SETTING",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    SMTP_SERVER = table.Column<string>(nullable: true),
                    SMTP_PORT = table.Column<int>(nullable: true),
                    SMTP_USERNAME = table.Column<string>(nullable: true),
                    SMTP_PASSWORD = table.Column<string>(nullable: true),
                    SENDER_EMAIL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAIL_SETTING", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SITE_SETTING",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    PAGE_TITLE = table.Column<string>(maxLength: 255, nullable: false),
                    PAGE_EMAIL = table.Column<string>(maxLength: 255, nullable: false),
                    DEFAULT_PAGE_SIZE = table.Column<int>(nullable: true),
                    PAGE_SIZE_OPTIONS = table.Column<string>(nullable: true),
                    SHOW_FOOTER = table.Column<bool>(nullable: false),
                    FOOTER_DATA = table.Column<string>(nullable: true),
                    ICON = table.Column<string>(nullable: true),
                    LOGO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITE_SETTING", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SSO_SETTING",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    ENABLE_GOOGLE_AUTH0 = table.Column<bool>(nullable: false),
                    GOOGLE_CLIENT_ID = table.Column<string>(nullable: true),
                    GOOGLE_CLIENT_SECRET = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSO_SETTING", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MAIL_SETTING");

            migrationBuilder.DropTable(
                name: "SITE_SETTING");

            migrationBuilder.DropTable(
                name: "SSO_SETTING");
        }
    }
}
