using Domain.Entities;

namespace Infrastructure.Repository;

public class LoginPasswordRepository(MyDbContext db):BaseRepository<LoginPassword>(db)
{
    
}