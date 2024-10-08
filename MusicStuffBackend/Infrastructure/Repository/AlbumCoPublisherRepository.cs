﻿using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AlbumCoPublisherRepository(MyDbContext db):BaseRepository<AlbumCoPublisher>(db)
{
    public override async Task<List<AlbumCoPublisher>> FindEntitiesByAsync(Expression<Func<AlbumCoPublisher, bool>> filter)
    {
        return await db.AlbumCoPublishers
            .Include(x=>x.Album)
            .Include(x=>x.User)
            .Where(filter).ToListAsync();
    }

    public override async Task<AlbumCoPublisher> FindEntityByAsync(Expression<Func<AlbumCoPublisher, bool>> filter)
    {
        return await db.AlbumCoPublishers
            .Include(x => x.Album)
            .Include(x => x.User)
            .FirstAsync(filter);
    }
}