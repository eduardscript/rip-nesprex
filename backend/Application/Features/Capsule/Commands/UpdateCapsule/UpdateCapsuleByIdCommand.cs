using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Capsule.Commands.UpdateCapsule;

public record UpdateCapsuleByIdCommand() : IRequest
{
}