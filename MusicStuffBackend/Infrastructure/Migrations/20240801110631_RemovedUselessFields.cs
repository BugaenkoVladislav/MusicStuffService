using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUselessFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Music_Users_IdCreator",
                table: "Music");

            migrationBuilder.DropIndex(
                name: "IX_Music_IdCreator",
                table: "Music");

            migrationBuilder.DropColumn(
                name: "IdCreator",
                table: "Music");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdCreator",
                table: "Music",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Music_IdCreator",
                table: "Music",
                column: "IdCreator");

            migrationBuilder.AddForeignKey(
                name: "FK_Music_Users_IdCreator",
                table: "Music",
                column: "IdCreator",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
