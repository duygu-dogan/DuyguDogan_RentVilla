using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReservationCartAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReservationCartItemId",
                table: "AddOns",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReservationCarts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReservationCartItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationCartId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AdultNumber = table.Column<int>(type: "integer", nullable: false),
                    ChildrenNumber = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReservationCartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCartItems_ReservationCarts_ReservationCartId",
                        column: x => x.ReservationCartId,
                        principalTable: "ReservationCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_ReservationCartItemId",
                table: "AddOns",
                column: "ReservationCartItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCartItems_ProductId",
                table: "ReservationCartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCartItems_ReservationCartId",
                table: "ReservationCartItems",
                column: "ReservationCartId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_UserId",
                table: "ReservationCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AddOns_ReservationCartItems_ReservationCartItemId",
                table: "AddOns",
                column: "ReservationCartItemId",
                principalTable: "ReservationCartItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddOns_ReservationCartItems_ReservationCartItemId",
                table: "AddOns");

            migrationBuilder.DropTable(
                name: "ReservationCartItems");

            migrationBuilder.DropTable(
                name: "ReservationCarts");

            migrationBuilder.DropIndex(
                name: "IX_AddOns_ReservationCartItemId",
                table: "AddOns");

            migrationBuilder.DropColumn(
                name: "ReservationCartItemId",
                table: "AddOns");
        }
    }
}
