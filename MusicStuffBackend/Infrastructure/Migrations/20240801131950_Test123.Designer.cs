﻿// <auto-generated />
using Infrastructure.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240801131950_Test123")]
    partial class Test123
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Domain.Entities.Album", b =>
                {
                    b.Property<long>("IdAlbum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdAlbum"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathPhoto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdAlbum");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Domain.Domain.Entities.AlbumCoPublisher", b =>
                {
                    b.Property<long>("IdAlbumCoPublisher")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdAlbumCoPublisher"));

                    b.Property<long>("IdAlbum")
                        .HasColumnType("bigint");

                    b.Property<long>("IdCoPublisher")
                        .HasColumnType("bigint");

                    b.HasKey("IdAlbumCoPublisher");

                    b.HasIndex("IdAlbum");

                    b.HasIndex("IdCoPublisher");

                    b.ToTable("AlbumCoPublishers");
                });

            modelBuilder.Entity("Domain.Domain.Entities.LoginPassword", b =>
                {
                    b.Property<long>("IdLoginPassword")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdLoginPassword"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdLoginPassword");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("LoginPasswords");
                });

            modelBuilder.Entity("Domain.Domain.Entities.Music", b =>
                {
                    b.Property<long>("IdMusic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdMusic"));

                    b.Property<double>("Duration")
                        .HasColumnType("double precision");

                    b.Property<long>("IdAlbum")
                        .HasColumnType("bigint");

                    b.Property<string>("NameOfTrack")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PathOfTrack")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdMusic");

                    b.HasIndex("IdAlbum");

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("Domain.Domain.Entities.Playlist", b =>
                {
                    b.Property<long>("IdPlaylist")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdPlaylist"));

                    b.Property<string>("PhotoPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PlaylistName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdPlaylist");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("Domain.Domain.Entities.PlaylistMusic", b =>
                {
                    b.Property<long>("IdPlaylistMusic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdPlaylistMusic"));

                    b.Property<long>("IdMusic")
                        .HasColumnType("bigint");

                    b.Property<long>("IdPlaylist")
                        .HasColumnType("bigint");

                    b.HasKey("IdPlaylistMusic");

                    b.HasIndex("IdMusic");

                    b.HasIndex("IdPlaylist");

                    b.ToTable("PlaylistMusics");
                });

            modelBuilder.Entity("Domain.Domain.Entities.PlaylistUser", b =>
                {
                    b.Property<long>("IdPlaylistUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdPlaylistUser"));

                    b.Property<long>("IdPlaylist")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsCreator")
                        .HasColumnType("boolean");

                    b.HasKey("IdPlaylistUser");

                    b.HasIndex("IdPlaylist");

                    b.HasIndex("IdUser");

                    b.ToTable("PlaylistUsers");
                });

            modelBuilder.Entity("Domain.Domain.Entities.Role", b =>
                {
                    b.Property<long>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdRole"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdRole");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Domain.Entities.TrackCoPublisher", b =>
                {
                    b.Property<long>("IdTrackCoPublisher")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdTrackCoPublisher"));

                    b.Property<long>("IdCoPublisher")
                        .HasColumnType("bigint");

                    b.Property<long>("IdTrack")
                        .HasColumnType("bigint");

                    b.HasKey("IdTrackCoPublisher");

                    b.HasIndex("IdCoPublisher");

                    b.HasIndex("IdTrack");

                    b.ToTable("TrackCoPublishers");
                });

            modelBuilder.Entity("Domain.Domain.Entities.User", b =>
                {
                    b.Property<long>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdUser"));

                    b.Property<long>("IdLoginPassword")
                        .HasColumnType("bigint");

                    b.Property<long>("IdRole")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdUser");

                    b.HasIndex("IdLoginPassword");

                    b.HasIndex("IdRole");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Domain.Entities.AlbumCoPublisher", b =>
                {
                    b.HasOne("Domain.Domain.Entities.Album", "Album")
                        .WithMany()
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdCoPublisher")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Domain.Entities.Music", b =>
                {
                    b.HasOne("Domain.Domain.Entities.Album", "Album")
                        .WithMany()
                        .HasForeignKey("IdAlbum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Domain.Domain.Entities.PlaylistMusic", b =>
                {
                    b.HasOne("Domain.Domain.Entities.Music", "Music")
                        .WithMany()
                        .HasForeignKey("IdMusic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Domain.Entities.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("IdPlaylist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Music");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("Domain.Domain.Entities.PlaylistUser", b =>
                {
                    b.HasOne("Domain.Domain.Entities.Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("IdPlaylist")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Domain.Entities.TrackCoPublisher", b =>
                {
                    b.HasOne("Domain.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdCoPublisher")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Domain.Entities.Music", "Track")
                        .WithMany()
                        .HasForeignKey("IdTrack")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Track");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Domain.Entities.LoginPassword", "LoginPassword")
                        .WithMany()
                        .HasForeignKey("IdLoginPassword")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginPassword");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
