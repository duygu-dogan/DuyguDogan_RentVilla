using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class attributesIdUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Attributes_AttributeDescId",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "AttributeDescId",
                table: "ProductAttributes",
                newName: "AttributesId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_AttributeDescId",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_AttributesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Attributes_AttributesId",
                table: "ProductAttributes",
                column: "AttributesId",
                principalTable: "Attributes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_Attributes_AttributesId",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "AttributesId",
                table: "ProductAttributes",
                newName: "AttributeDescId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_AttributesId",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_AttributeDescId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_Attributes_AttributeDescId",
                table: "ProductAttributes",
                column: "AttributeDescId",
                principalTable: "Attributes",
                principalColumn: "Id");
        }
    }
}
