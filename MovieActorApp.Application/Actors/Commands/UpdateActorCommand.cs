using MediatR;
using MovieActorApp.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Commands
{
    public record UpdateActorCommand(int Id, ActorRequest Dto) : IRequest<ActorResponse?>;
}
