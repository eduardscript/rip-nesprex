using Application.Repositories;
using MediatR;

namespace Application.Features.Capsule.Commands.DeleteCapsule;

public record DeleteCapsuleByIdCommand(int CapsuleId) : IRequest;

public class DeleteCapsuleByIdCommandHandler : IRequestHandler<DeleteCapsuleByIdCommand>
{
    private readonly ICapsuleRepository _capsuleRepository;

    public DeleteCapsuleByIdCommandHandler(ICapsuleRepository capsuleRepository)
    {
        _capsuleRepository = capsuleRepository;
    }

    public async Task Handle(DeleteCapsuleByIdCommand request, CancellationToken cancellationToken)
    {
        await _capsuleRepository.DeleteCapsuleById(request.CapsuleId);
    }
}
