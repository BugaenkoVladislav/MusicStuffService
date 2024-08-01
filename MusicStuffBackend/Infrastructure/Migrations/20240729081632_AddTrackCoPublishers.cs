using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackCoPublishers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_IdUser",
                table: "Playlists");

            migrationBuilder.DropIndex(
                name: "IX_Playlists_IdUser",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "CoPublishersIds",
                table: "Music");

            migrationBuilder.CreateTable(
                name: "TrackCoPublishers",
                columns: table => new
                {
                    IdTrackCoPublisher = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdTrack = table.Column<long>(type: "bigint", nullable: false),
                    IdCoPublisher = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCoPublishers", x => x.IdTrackCoPublisher);
                    table.ForeignKey(
                        name: "FK_TrackCoPublishers_Music_IdTrack",
                        column: x => x.IdTrack,
                        principalTable: "Music",
                        principalColumn: "IdMusic",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackCoPublishers_Users_IdCoPublisher",
                        column: x => x.IdCoPublisher,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackCoPublishers_IdCoPublisher",
                table: "TrackCoPublishers",
                column: "IdCoPublisher");

            migrationBuilder.CreateIndex(
                name: "IX_TrackCoPublishers_IdTrack",
                table: "TrackCoPublishers",
                column: "IdTrack");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackCoPublishers");

            migrationBuilder.AddColumn<long>(
                name: "IdUser",
                table: "Playlists",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<List<long>>(
                name: "CoPublishersIds",
                table: "Music",
                type: "bigint[]",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_IdUser",
                table: "Playlists",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_IdUser",
                table: "Playlists",
                column: "IdUser",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
