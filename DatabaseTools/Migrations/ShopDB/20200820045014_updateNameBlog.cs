using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations.ShopDB
{
    public partial class updateNameBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BLOG_IMAGE_Blogs_BLOG_ID",
                table: "BLOG_IMAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BLOG_CATEGORIES_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_PRODUCT_TYPES_CATEGORY_ID",
                table: "PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "BLOGS");

            migrationBuilder.RenameColumn(
                name: "CATEGORY_ID",
                table: "PRODUCTS",
                newName: "ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCTS_CATEGORY_ID",
                table: "PRODUCTS",
                newName: "IX_PRODUCTS_ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "BLOGS",
                newName: "IX_BLOGS_BlogCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductTypeId",
                table: "PRODUCTS",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BLOGS",
                table: "BLOGS",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BLOG_IMAGE_BLOGS_BLOG_ID",
                table: "BLOG_IMAGE",
                column: "BLOG_ID",
                principalTable: "BLOGS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BLOGS_BLOG_CATEGORIES_BlogCategoryId",
                table: "BLOGS",
                column: "BlogCategoryId",
                principalTable: "BLOG_CATEGORIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_PRODUCT_TYPES_ProductTypeId",
                table: "PRODUCTS",
                column: "ProductTypeId",
                principalTable: "PRODUCT_TYPES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BLOG_IMAGE_BLOGS_BLOG_ID",
                table: "BLOG_IMAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_BLOGS_BLOG_CATEGORIES_BlogCategoryId",
                table: "BLOGS");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_PRODUCT_TYPES_ProductTypeId",
                table: "PRODUCTS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BLOGS",
                table: "BLOGS");

            migrationBuilder.RenameTable(
                name: "BLOGS",
                newName: "Blogs");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "PRODUCTS",
                newName: "CATEGORY_ID");

            migrationBuilder.RenameIndex(
                name: "IX_PRODUCTS_ProductTypeId",
                table: "PRODUCTS",
                newName: "IX_PRODUCTS_CATEGORY_ID");

            migrationBuilder.RenameIndex(
                name: "IX_BLOGS_BlogCategoryId",
                table: "Blogs",
                newName: "IX_Blogs_BlogCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "CATEGORY_ID",
                table: "PRODUCTS",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BLOG_IMAGE_Blogs_BLOG_ID",
                table: "BLOG_IMAGE",
                column: "BLOG_ID",
                principalTable: "Blogs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_BLOG_CATEGORIES_BlogCategoryId",
                table: "Blogs",
                column: "BlogCategoryId",
                principalTable: "BLOG_CATEGORIES",
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
    }
}
