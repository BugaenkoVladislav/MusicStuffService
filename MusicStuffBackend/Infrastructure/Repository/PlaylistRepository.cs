using Domain.Domain.Entities;

namespace Infrastructure.Infrastructure.Repository;

public class PlaylistRepository(MyDbContext db):BaseRepository<Playlist>(db)
{
    
}