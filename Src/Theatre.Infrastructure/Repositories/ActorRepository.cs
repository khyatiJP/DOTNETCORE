

using Theatre.Application.Contracts;
using Theatre.Domain.Entities;
using Theatre.Infrastructure.Contracts;

namespace Theatre.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly IBaseRepository<Actors> _actorRepository;
        public ActorRepository(IBaseRepository<Actors> actorRepository)
        {
            _actorRepository = actorRepository;
        }
        public async Task<IEnumerable<Actors>> GetAll()
        {
            return await _actorRepository.GetAll();
        }
    }
}
