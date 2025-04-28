using MediatR;
using MovieActorApp.Application.DTO;
using MovieActorApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Movies.Queries
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieResponse?>
    {
        private readonly IMovieRepository _movieRepository;

        public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieResponse?> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await _movieRepository.GetByIdAsync(request.Id);
            return movie is null ? null : MovieResponse.FromEntity(movie);
        }
    }
}
