using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _1234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumCoPublishers",
                columns: table => new
                {
                    IdAlbumCoPublisher = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdAlbum = table.Column<long>(type: "bigint", nullable: false),
                    IdCoPublisher = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumCoPublishers", x => x.IdAlbumCoPublisher);
                    table.ForeignKey(
                        name: "FK_AlbumCoPublishers_Albums_IdAlbum",
                        column: x => x.IdAlbum,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumCoPublishers_Users_IdCoPublisher",
                        column: x => x.IdCoPublisher,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumCoPublishers_IdAlbum",
                table: "AlbumCoPublishers",
                column: "IdAlbum");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumCoPublishers_IdCoPublisher",
                table: "AlbumCoPublishers",
                column: "IdCoPublisher");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumCoPublishers");
        }
    }
}
