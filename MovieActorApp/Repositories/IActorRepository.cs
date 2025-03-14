using MovieActorApp.Models;
using MovieActorApp.Services.DTO;

namespace MovieActorApp.Repositories
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
