using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReservationCartUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationCarts_Reservations_ReservationId",
                table: "ReservationCarts");

            migrationBuilder.DropIndex(
                name: "IX_ReservationCarts_ReservationId",
                table: "ReservationCarts");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "ReservationCarts");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationCartId",
                table: "Reservations",
                column: "ReservationCartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationCarts_ReservationCartId",
                table: "Reservations",
                column: "ReservationCartId",
                principalTable: "ReservationCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationCarts_ReservationCartId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationCartId",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "ReservationCarts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCarts_ReservationId",
                table: "ReservationCarts",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationCarts_Reservations_ReservationId",
                table: "ReservationCarts",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
