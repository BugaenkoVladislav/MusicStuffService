using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    public DbSet<Album> Albums { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<LoginPassword> LoginPasswords { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistMusic> PlaylistMusics { get; set; }
    public DbSet<PlaylistUser> PlaylistUsers { get; set; }
    public DbSet<Role> Roles { get;set; }
    public DbSet<TrackCoPublisher> TrackCoPublishers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}