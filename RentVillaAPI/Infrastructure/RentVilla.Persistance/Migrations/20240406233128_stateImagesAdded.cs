using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class stateImagesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StateStateImageFile",
                columns: table => new
                {
                    StateImageFilesId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateStateImageFile", x => new { x.StateImageFilesId, x.StatesId });
                    table.ForeignKey(
                        name: "FK_StateStateImageFile_Files_StateImageFilesId",
                        column: x => x.StateImageFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateStateImageFile_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateStateImageFile_StatesId",
                table: "StateStateImageFile",
                column: "StatesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StateStateImageFile");
        }
    }
}
