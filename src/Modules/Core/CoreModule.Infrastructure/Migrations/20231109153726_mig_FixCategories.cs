using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreModule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_FixCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_categories_ParentId",
                schema: "dbo",
                table: "categories",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_categories_categories_ParentId",
                schema: "dbo",
                table: "categories",
                column: "ParentId",
                principalSchema: "dbo",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categories_categories_ParentId",
                schema: "dbo",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "IX_categories_ParentId",
                schema: "dbo",
                table: "categories");
        }
    }
}
