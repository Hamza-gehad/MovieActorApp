using MovieActorApp.Application.DTO;
using MovieActorApp.Domain.Models;

namespace MovieActorApp.Application.Interfaces
{
    public interface IActorRepository
    {

            Task<Actor> AddActorAsync(ActorRequest dto);
            Task<IEnumerable<Actor>> GetAllAsync();
            Task<Actor?> GetByIdAsync(int id);
            Task<bool> DeleteActorAsync(int id);
            Task<Actor?> UpdateActorAsync(int id, ActorRequest dto);


    }
}
