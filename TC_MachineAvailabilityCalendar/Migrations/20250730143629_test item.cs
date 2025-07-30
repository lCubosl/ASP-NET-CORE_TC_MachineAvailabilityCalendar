using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TC_MachineAvailabilityCalendar.Migrations
{
    /// <inheritdoc />
    public partial class testitem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ImageUrl", "Name" },
                values: new object[] { 1, "004.jpg", "TestMachine" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
