using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CartItemUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "ReservationCartItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "ReservationCartItems");
        }
    }
}
