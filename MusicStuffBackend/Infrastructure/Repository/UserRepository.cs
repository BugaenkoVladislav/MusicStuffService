using System.Linq.Expressions;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Infrastructure.Repository;

public class UserRepository(MyDbContext db) :BaseRepository<User>(db)
{
    public override async Task<User> FindEntityByAsync(Expression<Func<User, bool>> filter)
    {
        return await db.Users
            .Include(x=>x.LoginPassword)
            .Include(x=>x.Role)
            .FirstAsync(filter);
    }
}