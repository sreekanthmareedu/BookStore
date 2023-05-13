using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedAuthorIDandPublisherIDInBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "PublisherID");

            migrationBuilder.RenameColumn(
                name: "authors",
                table: "Books",
                newName: "AuthorID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_publishers",
                table: "Books",
                newName: "IX_Books_PublisherID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_authors",
                table: "Books",
                newName: "IX_Books_AuthorID");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 13, 10, 0, 54, 295, DateTimeKind.Local).AddTicks(7130), new DateTime(2023, 5, 13, 10, 0, 54, 295, DateTimeKind.Local).AddTicks(7141) });

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books",
                column: "AuthorID",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Publishers_PublisherID",
                table: "Books",
                column: "PublisherID",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Publishers_PublisherID",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "PublisherID",
                table: "Books",
                newName: "publishers");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Books",
                newName: "authors");

            migrationBuilder.RenameIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                newName: "IX_Books_publishers");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                newName: "IX_Books_authors");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2023, 5, 13, 1, 12, 41, 815, DateTimeKind.Local).AddTicks(2627), new DateTime(2023, 5, 13, 1, 12, 41, 815, DateTimeKind.Local).AddTicks(2642) });

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
    }
}
