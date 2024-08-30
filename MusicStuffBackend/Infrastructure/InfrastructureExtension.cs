using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureExtension
{
    public static IServiceCollection InfrastructureExt(this IServiceCollection services)
    {
        return services.AddDbContext<MyDbContext>(options => options.UseNpgsql(""));
    }
}