using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReservationCartUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationCarts_ReservationCartId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ReservationCartId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ReservationCartId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ReservationCarts_Id",
                table: "Reservations",
                column: "Id",
                principalTable: "ReservationCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ReservationCarts_Id",
                table: "Reservations");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationCartId",
                table: "Reservations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
