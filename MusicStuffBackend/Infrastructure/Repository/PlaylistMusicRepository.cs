using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class PlaylistMusicRepository(MyDbContext db):BaseRepository<PlaylistMusic>(db)
{
    public override async Task<List<PlaylistMusic>> FindEntitiesByAsync(Expression<Func<PlaylistMusic, bool>> filter)
    {
        return await db.PlaylistMusics
            .Include(x => x.Music)
                .ThenInclude(x=>x.Album)
            .Include(x=>x.Music)
            .Where(filter).ToListAsync();
    }
}