using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Music_Albums_IdAlbum",
                table: "Music");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMusics_Music_IdMusic",
                table: "PlaylistMusics");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackCoPublishers_Music_IdTrack",
                table: "TrackCoPublishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Music",
                table: "Music");

            migrationBuilder.RenameTable(
                name: "Music",
                newName: "Musics");

            migrationBuilder.RenameIndex(
                name: "IX_Music_IdAlbum",
                table: "Musics",
                newName: "IX_Musics_IdAlbum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musics",
                table: "Musics",
                column: "IdMusic");

            migrationBuilder.AddForeignKey(
                name: "FK_Musics_Albums_IdAlbum",
                table: "Musics",
                column: "IdAlbum",
                principalTable: "Albums",
                principalColumn: "IdAlbum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMusics_Musics_IdMusic",
                table: "PlaylistMusics",
                column: "IdMusic",
                principalTable: "Musics",
                principalColumn: "IdMusic",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackCoPublishers_Musics_IdTrack",
                table: "TrackCoPublishers",
                column: "IdTrack",
                principalTable: "Musics",
                principalColumn: "IdMusic",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Musics_Albums_IdAlbum",
                table: "Musics");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistMusics_Musics_IdMusic",
                table: "PlaylistMusics");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackCoPublishers_Musics_IdTrack",
                table: "TrackCoPublishers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Musics",
                table: "Musics");

            migrationBuilder.RenameTable(
                name: "Musics",
                newName: "Music");

            migrationBuilder.RenameIndex(
                name: "IX_Musics_IdAlbum",
                table: "Music",
                newName: "IX_Music_IdAlbum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Music",
                table: "Music",
                column: "IdMusic");

            migrationBuilder.AddForeignKey(
                name: "FK_Music_Albums_IdAlbum",
                table: "Music",
                column: "IdAlbum",
                principalTable: "Albums",
                principalColumn: "IdAlbum",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistMusics_Music_IdMusic",
                table: "PlaylistMusics",
                column: "IdMusic",
                principalTable: "Music",
                principalColumn: "IdMusic",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackCoPublishers_Music_IdTrack",
                table: "TrackCoPublishers",
                column: "IdTrack",
                principalTable: "Music",
                principalColumn: "IdMusic",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
