using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Movies.Commands
{
    public record DeleteMovieCommand(int Id) : IRequest<bool>;

}
