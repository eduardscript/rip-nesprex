using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.Extensions;

internal static class EfCoreExtensions
{
    internal static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specification>()
            .HasData(new List<Specification>
            {
                new(Size.Ristretto, 25) { Id = Size.Ristretto.ToIncrementedInt() },
                new(Size.Espresso, 40) { Id = Size.Espresso.ToIncrementedInt() },
                new(Size.DoubleEspresso, 80) { Id = Size.DoubleEspresso.ToIncrementedInt() },
                new(Size.GranLungo, 150) { Id = Size.GranLungo.ToIncrementedInt() },
                new(Size.Mug, 230) { Id = Size.Mug.ToIncrementedInt() },
                new(Size.Carafe, 500) { Id = Size.Carafe.ToIncrementedInt() },
            });
    }
}

public static class EnumExtensions
{
    public static int ToIncrementedInt(this Enum @enum)
    {
        return Convert.ToInt32(@enum) + 1;
    }
}