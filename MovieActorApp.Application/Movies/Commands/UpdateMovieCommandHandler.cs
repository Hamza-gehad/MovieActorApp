using MediatR;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Movies.Commands
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, MovieResponse?>
    {
        private readonly IMovieRepository _movieRepository;

        public UpdateMovieCommandHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse?> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var updated = await _movieRepository.UpdateMovieAsync(request.Id, request.Dto);
            return updated is null ? null : MovieResponse.FromEntity(updated);
        }
    }

}
