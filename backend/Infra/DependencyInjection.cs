using Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

// ReSharper disable CheckNamespace
namespace Microsoft.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfra(this IServiceCollection services)
    {
        services
            .AddEfCore()
            .AddRepositories()
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
    }

    private static IServiceCollection AddEfCore(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb");
        });

        services.BuildServiceProvider().GetService<AppDbContext>()!.Database.EnsureCreated();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var repositoriesAbstractions = Application.AssemblyReference.Assembly
            .GetTypes()
            .Where(x => x.IsInterface && x.Name.EndsWith("Repository"))
            .OrderBy(x => x.Name)
            .ToArray();


        var repositoriesImplementations = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.IsClass && x.Name.EndsWith("Repository"))
            .OrderBy(x => x.Name)
            .ToArray();

        for (var i = 0; i < repositoriesAbstractions.Length; i++)
        {
            services.AddScoped(repositoriesAbstractions[i], repositoriesImplementations[i]);
        }

        return services;
    }
}