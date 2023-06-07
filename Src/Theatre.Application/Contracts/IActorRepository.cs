using Theatre.Domain.Entities;

namespace Theatre.Application.Contracts
{
    public interface IActorRepository
    {
        Task<IEnumerable<Actors>> GetAll();
    }
}
