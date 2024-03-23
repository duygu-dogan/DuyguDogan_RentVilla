using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AttributeRelationDrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeTypeName");

            migrationBuilder.AddColumn<Guid>(
                name: "AttributeTypeId",
                table: "Attributes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AttributeTypeId",
                table: "Attributes",
                column: "AttributeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_AttributeTypes_AttributeTypeId",
                table: "Attributes",
                column: "AttributeTypeId",
                principalTable: "AttributeTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_AttributeTypes_AttributeTypeId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_AttributeTypeId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributeTypeId",
                table: "Attributes");

            migrationBuilder.CreateTable(
                name: "AttributeTypeName",
                columns: table => new
                {
                    AttributesId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeTypeName", x => new { x.AttributesId, x.AttributeTypeId });
                    table.ForeignKey(
                        name: "FK_AttributeTypeName_AttributeTypes_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeTypeName_Attributes_AttributesId",
                        column: x => x.AttributesId,
                        principalTable: "Attributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeTypeName_AttributesId",
                table: "AttributeTypeName",
                column: "AttributesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeTypeName_AttributeTypeId",
                table: "AttributeTypeName",
                column: "AttributeTypeId",
                unique: true);
        }
    }
}
