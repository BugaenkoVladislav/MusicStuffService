using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AlbumRepository(MyDbContext db) : BaseRepository<Album>(db)
{
    private readonly MyDbContext _db = db;

    public override async Task<Album> FindEntityByAsync(Expression<Func<Album, bool>> filter)
    {
        return await _db.Albums.FirstAsync(filter);
    }
}
