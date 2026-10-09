using Microsoft.Extensions.DependencyInjection;
using Soccer.BLL.Profiles;

namespace Soccer.BLL.Infrastructure;

public static class AutoMapperServiceExtensions
{
    public static void AddSoccerAutoMapper(this IServiceCollection services)
    {
        // Передаємо очікуваний Action<IMapperConfigurationExpression> 
        // і реєструємо профіль через метод AddProfile
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<SoccerProfile>();
        });
    }
}