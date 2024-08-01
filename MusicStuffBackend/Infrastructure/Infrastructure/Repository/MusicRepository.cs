using System.Linq.Expressions;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class MusicRepository(MyDbContext db):BaseRepository<Music>(db)
{
    public override async Task<List<Music>> FindEntitiesByAsync(Expression<Func<Music, bool>> filter)
    {
        return await db.Musics.Include(x=> x.Album).Where(filter).ToListAsync();
    }
}