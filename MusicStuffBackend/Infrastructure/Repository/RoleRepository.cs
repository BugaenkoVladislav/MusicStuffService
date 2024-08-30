using Domain.Domain.Entities;

namespace Infrastructure.Infrastructure.Repository;

public class RoleRepository(MyDbContext db):BaseRepository<Role>(db)
{
    
}