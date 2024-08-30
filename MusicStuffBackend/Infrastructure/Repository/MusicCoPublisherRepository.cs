using System.Linq.Expressions;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class MusicCoPublisherRepository(MyDbContext db):BaseRepository<TrackCoPublisher>(db)
{
    public override async Task<List<TrackCoPublisher>> FindEntitiesByAsync(Expression<Func<TrackCoPublisher, bool>> filter)
    {
        return await db.TrackCoPublishers
            .Include(x=>x.Track)
            .Include(x=>x.User)
            .Where(filter).ToListAsync();
    }

    public override async Task<TrackCoPublisher> FindEntityByAsync(Expression<Func<TrackCoPublisher, bool>> filter)
    {
        return await db.TrackCoPublishers
            .Include(x=>x.Track)
            .Include(x=>x.User)
            .FirstAsync(filter);
    }
}