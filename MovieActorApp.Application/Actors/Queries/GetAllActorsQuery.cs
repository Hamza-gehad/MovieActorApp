using MediatR;
using MovieActorApp.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Queries
{
    public record GetAllActorsQuery() : IRequest<IEnumerable<ActorResponse>>;
}
