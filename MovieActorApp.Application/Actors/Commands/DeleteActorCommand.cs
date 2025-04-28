using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieActorApp.Application.Actors.Commands
{
    public record DeleteActorCommand(int Id) : IRequest<bool>;
}
