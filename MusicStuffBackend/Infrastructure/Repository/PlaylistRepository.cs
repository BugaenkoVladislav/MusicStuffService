using Domain.Entities;

namespace Infrastructure.Repository;

public class PlaylistRepository(MyDbContext db):BaseRepository<Playlist>(db)
{
    
}