using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductAddressUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductAddresses_ProductAddressId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductAddressId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductAddressId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductAddresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductAddresses_ProductId",
                table: "ProductAddresses",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAddresses_Products_ProductId",
                table: "ProductAddresses",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAddresses_Products_ProductId",
                table: "ProductAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ProductAddresses_ProductId",
                table: "ProductAddresses");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductAddresses");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductAddressId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductAddressId",
                table: "Products",
                column: "ProductAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductAddresses_ProductAddressId",
                table: "Products",
                column: "ProductAddressId",
                principalTable: "ProductAddresses",
                principalColumn: "Id");
        }
    }
}
