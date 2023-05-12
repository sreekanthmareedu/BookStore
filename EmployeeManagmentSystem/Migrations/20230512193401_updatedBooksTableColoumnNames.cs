using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedBooksTableColoumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_Author",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_Publisher",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Publisher",
                table: "Books",
                newName: "publishers");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Books",
                newName: "authors");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Publisher",
                table: "Books",
                newName: "IX_Books_publishers");

            migrationBuilder.RenameIndex(
                name: "IX_Books_Author",
                table: "Books",
                newName: "IX_Books_authors");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 13, 1, 4, 1, 493, DateTimeKind.Local).AddTicks(8243), new DateTime(2023, 5, 13, 1, 4, 1, 493, DateTimeKind.Local).AddTicks(8253) });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_authors",
                table: "Books",
                column: "authors",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_publishers",
                table: "Books",
                column: "publishers",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_authors",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_publishers",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "publishers",
                table: "Books",
                newName: "Publisher");

            migrationBuilder.RenameColumn(
                name: "authors",
                table: "Books",
                newName: "Author");

            migrationBuilder.RenameIndex(
                name: "IX_Books_publishers",
                table: "Books",
                newName: "IX_Books_Publisher");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authors",
                table: "Books",
                newName: "IX_Books_Author");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 12, 19, 6, 44, 8, DateTimeKind.Local).AddTicks(4769), new DateTime(2023, 5, 12, 19, 6, 44, 8, DateTimeKind.Local).AddTicks(4780) });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_Author",
                table: "Books",
                column: "Author",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_Publisher",
                table: "Books",
                column: "Publisher",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
