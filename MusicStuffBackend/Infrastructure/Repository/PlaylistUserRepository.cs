using Domain.Domain.Entities;

namespace Infrastructure.Infrastructure.Repository;

public class PlaylistUserRepository(MyDbContext db): BaseRepository<PlaylistUser>(db)
{
    
}