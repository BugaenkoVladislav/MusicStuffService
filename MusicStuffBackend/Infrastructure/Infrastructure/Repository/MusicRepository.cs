using Domain.Domain.Entities;

namespace Infrastructure.Infrastructure.Repository;

public class MusicRepository(MyDbContext db):BaseRepository<Music>(db)
{
    
}