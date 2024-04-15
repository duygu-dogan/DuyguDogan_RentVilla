using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentVilla.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class endpointEntityUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Definition",
                table: "Endpoints",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Definition",
                table: "Endpoints");
        }
    }
}
