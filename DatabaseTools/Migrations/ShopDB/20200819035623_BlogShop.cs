using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations.ShopDB
{
    public partial class BlogShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                table: "PRODUCTS");

            migrationBuilder.AddColumn<string>(
                name: "CategoryId",
                table: "PRODUCTS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialId",
                table: "PRODUCTS",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_AT = table.Column<DateTime>(nullable: true),
                    LAST_UPDATE_AT = table.Column<DateTime>(nullable: true),
                    CREATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_UPDATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    SLUG = table.Column<string>(maxLength: 255, nullable: false),
                    TITLE = table.Column<string>(maxLength: 255, nullable: false),
                    CONTENT = table.Column<string>(maxLength: 255, nullable: false),
                    BlogCategoryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Blog_BLOG_CATEGORIES_BlogCategoryId",
                        column: x => x.BlogCategoryId,
                        principalTable: "BLOG_CATEGORIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BLOG_IMAGE",
                columns: table => new
                {
                    ID = table.Column<string>(maxLength: 50, nullable: false),
                    CREATE_AT = table.Column<DateTime>(nullable: true),
                    LAST_UPDATE_AT = table.Column<DateTime>(nullable: true),
                    CREATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    LAST_UPDATE_BY = table.Column<string>(maxLength: 50, nullable: true),
                    URL = table.Column<string>(nullable: true),
                    THUMB_URL = table.Column<string>(nullable: true),
                    FILE_NAME = table.Column<string>(nullable: true),
                    FILE_SIZE = table.Column<long>(nullable: true),
                    FILE_TYPE = table.Column<string>(nullable: true),
                    BLOG_ID = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLOG_IMAGE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BLOG_IMAGE_Blog_BLOG_ID",
                        column: x => x.BLOG_ID,
                        principalTable: "Blog",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_CategoryId",
                table: "PRODUCTS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_MaterialId",
                table: "PRODUCTS",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_BlogCategoryId",
                table: "Blog",
                column: "BlogCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BLOG_IMAGE_BLOG_ID",
                table: "BLOG_IMAGE",
                column: "BLOG_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CategoryId",
                table: "PRODUCTS",
                column: "CategoryId",
                principalTable: "CATEGORIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_MATERIALS_MaterialId",
                table: "PRODUCTS",
                column: "MaterialId",
                principalTable: "MATERIALS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_PRODUCT_TYPES_CATEGORY_ID",
                table: "PRODUCTS",
                column: "CATEGORY_ID",
                principalTable: "PRODUCT_TYPES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CategoryId",
                table: "PRODUCTS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_MATERIALS_MaterialId",
                table: "PRODUCTS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_PRODUCT_TYPES_CATEGORY_ID",
                table: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "BLOG_IMAGE");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCTS_CategoryId",
                table: "PRODUCTS");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCTS_MaterialId",
                table: "PRODUCTS");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PRODUCTS");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "PRODUCTS");

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                table: "PRODUCTS",
                column: "CATEGORY_ID",
                principalTable: "CATEGORIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
