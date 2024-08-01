using System.Linq.Expressions;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class AlbumRepository(MyDbContext db) : BaseRepository<Album>(db)
{
    
}
