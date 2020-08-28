using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseTools.Migrations.ShopDB
{
    public partial class NewRelationShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_BLOG_CATEGORIES_BlogCategoryId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_BLOG_IMAGE_Blog_BLOG_ID",
                table: "BLOG_IMAGE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "CUSTOMER_NAME",
                table: "CUSTOMER_FEEDBACK");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_BlogCategoryId",
                table: "Blogs",
                newName: "IX_Blogs_BlogCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "PRODUCT_REVIEW",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductId",
                table: "PRODUCT_REVIEW",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_ID",
                table: "CUSTOMER_FEEDBACK",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_REVIEW_CustomerId",
                table: "PRODUCT_REVIEW",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_REVIEW_ProductId",
                table: "PRODUCT_REVIEW",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMER_FEEDBACK_CUSTOMER_ID",
                table: "CUSTOMER_FEEDBACK",
                column: "CUSTOMER_ID");

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
                name: "FK_CUSTOMER_FEEDBACK_CUSTOMERS_CUSTOMER_ID",
                table: "CUSTOMER_FEEDBACK",
                column: "CUSTOMER_ID",
                principalTable: "CUSTOMERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_REVIEW_CUSTOMERS_CustomerId",
                table: "PRODUCT_REVIEW",
                column: "CustomerId",
                principalTable: "CUSTOMERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCT_REVIEW_PRODUCTS_ProductId",
                table: "PRODUCT_REVIEW",
                column: "ProductId",
                principalTable: "PRODUCTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BLOG_IMAGE_Blogs_BLOG_ID",
                table: "BLOG_IMAGE");

            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_BLOG_CATEGORIES_BlogCategoryId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_CUSTOMER_FEEDBACK_CUSTOMERS_CUSTOMER_ID",
                table: "CUSTOMER_FEEDBACK");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_REVIEW_CUSTOMERS_CustomerId",
                table: "PRODUCT_REVIEW");

            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCT_REVIEW_PRODUCTS_ProductId",
                table: "PRODUCT_REVIEW");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_REVIEW_CustomerId",
                table: "PRODUCT_REVIEW");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCT_REVIEW_ProductId",
                table: "PRODUCT_REVIEW");

            migrationBuilder.DropIndex(
                name: "IX_CUSTOMER_FEEDBACK_CUSTOMER_ID",
                table: "CUSTOMER_FEEDBACK");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "PRODUCT_REVIEW");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PRODUCT_REVIEW");

            migrationBuilder.DropColumn(
                name: "CUSTOMER_ID",
                table: "CUSTOMER_FEEDBACK");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_BlogCategoryId",
                table: "Blog",
                newName: "IX_Blog_BlogCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "CUSTOMER_NAME",
                table: "CUSTOMER_FEEDBACK",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_BLOG_CATEGORIES_BlogCategoryId",
                table: "Blog",
                column: "BlogCategoryId",
                principalTable: "BLOG_CATEGORIES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BLOG_IMAGE_Blog_BLOG_ID",
                table: "BLOG_IMAGE",
                column: "BLOG_ID",
                principalTable: "Blog",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
