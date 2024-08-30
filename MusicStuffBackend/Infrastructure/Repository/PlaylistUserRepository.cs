using Domain.Entities;

namespace Infrastructure.Repository;

public class PlaylistUserRepository(MyDbContext db): BaseRepository<PlaylistUser>(db)
{
    
}