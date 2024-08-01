using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Users_IdCreator",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_IdCreator",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "CoPublishersIds",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "IdCreator",
                table: "Albums");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<long>>(
                name: "CoPublishersIds",
                table: "Albums",
                type: "bigint[]",
                nullable: false);

            migrationBuilder.AddColumn<long>(
                name: "IdCreator",
                table: "Albums",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_IdCreator",
                table: "Albums",
                column: "IdCreator");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Users_IdCreator",
                table: "Albums",
                column: "IdCreator",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
