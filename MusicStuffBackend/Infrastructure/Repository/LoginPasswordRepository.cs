using Domain.Domain.Entities;

namespace Infrastructure.Infrastructure.Repository;

public class LoginPasswordRepository(MyDbContext db):BaseRepository<LoginPassword>(db)
{
    
}