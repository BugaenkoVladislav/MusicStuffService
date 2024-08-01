using System.Linq.Expressions;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class AlbumCoPublisherRepository(MyDbContext db):BaseRepository<AlbumCoPublisher>(db)
{
    public override async Task<List<AlbumCoPublisher>> FindEntitiesByAsync(Expression<Func<AlbumCoPublisher, bool>> filter)
    {
        return await db.AlbumCoPublishers
            .Include(x=>x.Album)
            .Include(x=>x.User)
            .Where(filter).ToListAsync();
    }

}