using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repository;

namespace Infrastructure;

public class UnitOfWork(MyDbContext db,AlbumCoPublisherRepository albumCoPublisherRepository,MusicCoPublisherRepository trackMusicCoPublisherRepository,AlbumRepository albumRepository, PlaylistRepository playlistRepository, PlaylistMusicRepository playlistMusicRepository, UserRepository userRepository, MusicRepository musicRepository, LoginPasswordRepository loginPasswordRepository, RoleRepository roleRepository, PlaylistUserRepository playlistUserRepository)
{
    private readonly MyDbContext _db = db;
    public IRepository<Playlist> PlaylistRepository { get; set; } = playlistRepository;
    public IRepository<PlaylistMusic> PlaylistMusicRepository { get; set; } = playlistMusicRepository;
    public IRepository<User> UserRepository { get; set; } = userRepository;
    public IRepository<Music> MusicRepository { get; set; } = musicRepository;
    public IRepository<LoginPassword> LoginPasswordRepository { get; set; } = loginPasswordRepository;
    public IRepository<Role> RoleRepository { get; set; } = roleRepository;
    public IRepository<PlaylistUser> PlaylistUserRepository { get; set; } = playlistUserRepository;
    public IRepository<Album> AlbumRepository { get; set; } = albumRepository;
    public IRepository<TrackCoPublisher> TrackMusicCoPublisherRepository { get; set; } = trackMusicCoPublisherRepository;
    public IRepository<AlbumCoPublisher> AlbumCoPublisherRepository { get; set; } = albumCoPublisherRepository;
    public async Task SaveAllChanges()
    {
        await _db.SaveChangesAsync();
    }
}