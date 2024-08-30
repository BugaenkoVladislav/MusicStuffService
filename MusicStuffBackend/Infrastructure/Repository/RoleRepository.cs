using Domain.Entities;

namespace Infrastructure.Repository;

public class RoleRepository(MyDbContext db):BaseRepository<Role>(db)
{
    
}