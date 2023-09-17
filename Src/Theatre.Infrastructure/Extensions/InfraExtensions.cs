using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Theatre.Application.Contracts;
using Theatre.Infrastructure.Contracts;
using Theatre.Infrastructure.Provider;
using Theatre.Infrastructure.Repositories;

namespace Theatre.Infrastructure.Extensions
{
    public static class InfraExtensions
    {
        public static void AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<IActorRepository, ActorRepository>();
            service.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            service.AddSingleton<RedisProvider>();
        }
    }
}
