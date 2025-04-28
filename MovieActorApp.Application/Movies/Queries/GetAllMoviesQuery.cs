using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieActorApp.Application.DTO;
namespace MovieActorApp.Application.Movies.Queries
{
    public record GetAllMoviesQuery : IRequest<IEnumerable<MovieResponse>>;

}
