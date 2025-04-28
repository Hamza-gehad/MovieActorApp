using MovieActorApp.Application.DTO;

namespace MovieActorApp.Application.Interfaces
{
    public interface IActorService
    {
        Task<ActorResponse> CreateActorAsync(ActorRequest dto);
        Task<IEnumerable<ActorResponse>> GetAllActorsAsync();
        Task<ActorResponse?> GetActorByIdAsync(int id);
        Task<bool> DeleteActorAsync(int id);
        Task<ActorResponse?> UpdateActorAsync(int id, ActorRequest dto);
    }
}
