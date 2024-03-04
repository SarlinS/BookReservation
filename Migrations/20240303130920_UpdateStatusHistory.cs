using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookReservation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStatusHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStatusHistory_BookItems_BookItemId",
                table: "BookStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookStatusHistory",
                table: "BookStatusHistory");

            migrationBuilder.RenameTable(
                name: "BookStatusHistory",
                newName: "BookStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_BookStatusHistory_BookItemId",
                table: "BookStatusHistories",
                newName: "IX_BookStatusHistories_BookItemId");

            migrationBuilder.AlterColumn<long>(
                name: "BookItemId",
                table: "BookStatusHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookStatusHistories",
                table: "BookStatusHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStatusHistories_BookItems_BookItemId",
                table: "BookStatusHistories",
                column: "BookItemId",
                principalTable: "BookItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookStatusHistories_BookItems_BookItemId",
                table: "BookStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookStatusHistories",
                table: "BookStatusHistories");

            migrationBuilder.RenameTable(
                name: "BookStatusHistories",
                newName: "BookStatusHistory");

            migrationBuilder.RenameIndex(
                name: "IX_BookStatusHistories_BookItemId",
                table: "BookStatusHistory",
                newName: "IX_BookStatusHistory_BookItemId");

            migrationBuilder.AlterColumn<long>(
                name: "BookItemId",
                table: "BookStatusHistory",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookStatusHistory",
                table: "BookStatusHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookStatusHistory_BookItems_BookItemId",
                table: "BookStatusHistory",
                column: "BookItemId",
                principalTable: "BookItems",
                principalColumn: "Id");
        }
    }
}
