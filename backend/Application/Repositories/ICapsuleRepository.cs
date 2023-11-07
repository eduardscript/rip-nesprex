namespace Application.Repositories;

public interface ICapsuleRepository
{
    Task<int> CreateCapsule(Entities.Capsule capsule);

    Task UpdateCapsule(Entities.Capsule capsule);

    Task<Entities.Capsule?> GetCapsuleById(int capsuleId);

    Task DeleteCapsuleById(int capsuleId);
}