using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReservation.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookStatusHistory",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsReserved = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChangeDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BookItemId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookStatusHistory_BookItems_BookItemId",
                        column: x => x.BookItemId,
                        principalTable: "BookItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookStatusHistory_BookItemId",
                table: "BookStatusHistory",
                column: "BookItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookStatusHistory");
        }
    }
}
