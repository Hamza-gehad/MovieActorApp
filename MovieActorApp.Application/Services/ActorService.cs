using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;

namespace MovieActorApp.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<ActorResponse> CreateActorAsync(ActorRequest dto)
        {
            var actor = await _actorRepository.AddActorAsync(dto);
            return ActorResponse.FromEntity(actor);
        }

        public async Task<IEnumerable<ActorResponse>> GetAllActorsAsync()
        {
            var actors = await _actorRepository.GetAllAsync();
            return actors.Select(ActorResponse.FromEntity);
        }

        public async Task<ActorResponse?> GetActorByIdAsync(int id)
        {
            var actor = await _actorRepository.GetByIdAsync(id);
            return actor is null ? null : ActorResponse.FromEntity(actor);
        }
        public async Task<bool> DeleteActorAsync(int id)
        {
            return await _actorRepository.DeleteActorAsync(id);
        }

        public async Task<ActorResponse?> UpdateActorAsync(int id, ActorRequest dto)
        {
            var updatedActor = await _actorRepository.UpdateActorAsync(id, dto);
            return updatedActor is null ? null : ActorResponse.FromEntity(updatedActor);
        }


    }

}
