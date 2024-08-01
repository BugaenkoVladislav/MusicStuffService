using System.Linq.Expressions;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class AlbumRepository(MyDbContext db) : BaseRepository<Album>(db)
{
    public override async Task<Album> FindEntityByAsync(Expression<Func<Album, bool>> filter)
    {
        return await db.Albums.Include(x=>x.Creator).FirstAsync(filter);
    }
}
