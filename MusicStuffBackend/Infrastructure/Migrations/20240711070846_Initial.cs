using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginPasswords",
                columns: table => new
                {
                    IdLoginPassword = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginPasswords", x => x.IdLoginPassword);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    IdRole = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdRole = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    IdLoginPassword = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_Users_LoginPasswords_IdLoginPassword",
                        column: x => x.IdLoginPassword,
                        principalTable: "LoginPasswords",
                        principalColumn: "IdLoginPassword",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    IdAlbum = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdCreator = table.Column<long>(type: "bigint", nullable: false),
                    PathPhoto = table.Column<string>(type: "text", nullable: false),
                    CoPublishersIds = table.Column<List<long>>(type: "bigint[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.IdAlbum);
                    table.ForeignKey(
                        name: "FK_Albums_Users_IdCreator",
                        column: x => x.IdCreator,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    IdPlaylist = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlaylistName = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.IdPlaylist);
                    table.ForeignKey(
                        name: "FK_Playlists_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    IdMusic = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameOfTrack = table.Column<string>(type: "text", nullable: false),
                    IdCreator = table.Column<long>(type: "bigint", nullable: false),
                    IdAlbum = table.Column<long>(type: "bigint", nullable: false),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    PathOfTrack = table.Column<string>(type: "text", nullable: false),
                    CoPublishersIds = table.Column<List<long>>(type: "bigint[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.IdMusic);
                    table.ForeignKey(
                        name: "FK_Music_Albums_IdAlbum",
                        column: x => x.IdAlbum,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Music_Users_IdCreator",
                        column: x => x.IdCreator,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistUsers",
                columns: table => new
                {
                    IdPlaylistUser = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPlaylist = table.Column<long>(type: "bigint", nullable: false),
                    IdUser = table.Column<long>(type: "bigint", nullable: false),
                    IsCreator = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistUsers", x => x.IdPlaylistUser);
                    table.ForeignKey(
                        name: "FK_PlaylistUsers_Playlists_IdPlaylist",
                        column: x => x.IdPlaylist,
                        principalTable: "Playlists",
                        principalColumn: "IdPlaylist",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistUsers_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistMusics",
                columns: table => new
                {
                    IdPlaylistMusic = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPlaylist = table.Column<long>(type: "bigint", nullable: false),
                    IdMusic = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistMusics", x => x.IdPlaylistMusic);
                    table.ForeignKey(
                        name: "FK_PlaylistMusics_Music_IdMusic",
                        column: x => x.IdMusic,
                        principalTable: "Music",
                        principalColumn: "IdMusic",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistMusics_Playlists_IdPlaylist",
                        column: x => x.IdPlaylist,
                        principalTable: "Playlists",
                        principalColumn: "IdPlaylist",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_IdCreator",
                table: "Albums",
                column: "IdCreator");

            migrationBuilder.CreateIndex(
                name: "IX_LoginPasswords_Login",
                table: "LoginPasswords",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Music_IdAlbum",
                table: "Music",
                column: "IdAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_Music_IdCreator",
                table: "Music",
                column: "IdCreator");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusics_IdMusic",
                table: "PlaylistMusics",
                column: "IdMusic");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistMusics_IdPlaylist",
                table: "PlaylistMusics",
                column: "IdPlaylist");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_IdUser",
                table: "Playlists",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistUsers_IdPlaylist",
                table: "PlaylistUsers",
                column: "IdPlaylist");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistUsers_IdUser",
                table: "PlaylistUsers",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdLoginPassword",
                table: "Users",
                column: "IdLoginPassword");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdRole",
                table: "Users",
                column: "IdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistMusics");

            migrationBuilder.DropTable(
                name: "PlaylistUsers");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LoginPasswords");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
