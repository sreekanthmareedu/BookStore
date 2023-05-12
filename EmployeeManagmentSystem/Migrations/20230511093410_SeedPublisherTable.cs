using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedPublisherTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedDate", "Email", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2023, 5, 11, 15, 4, 10, 500, DateTimeKind.Local).AddTicks(320), "Test@gmail.com", "Test", new DateTime(2023, 5, 11, 15, 4, 10, 500, DateTimeKind.Local).AddTicks(331) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
