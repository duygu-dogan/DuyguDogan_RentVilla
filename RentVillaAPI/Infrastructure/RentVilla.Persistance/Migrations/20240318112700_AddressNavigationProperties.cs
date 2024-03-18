using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddressNavigationProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAddressId",
                table: "User",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductAddressId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserAddressId",
                table: "User",
                column: "UserAddressId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserAddresses_UserAddressId",
                table: "User",
                column: "UserAddressId",
                principalTable: "UserAddresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductAddresses_ProductAddressId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserAddresses_UserAddressId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserAddressId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductAddressId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserAddressId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProductAddressId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Products",
                type: "text",
                nullable: true);
        }
    }
}
