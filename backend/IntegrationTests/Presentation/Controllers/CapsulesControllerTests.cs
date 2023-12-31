using System.Net;
using System.Net.Http.Json;
using Application.Exceptions;
using Application.Features.Capsule.Commands.CreateCapsule;
using Application.Features.Capsule.Queries.GetCapsuleById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Presentation.Controllers;

public class CapsulesControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly IMediator _mediator;

    public CapsulesControllerTests(
        CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _mediator = _factory.Services.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task GetCapsuleById_Success()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createCommand = new CreateCapsuleCommand(
            "Test_Capsule",
            "Portugal",
            1,
            Size.Espresso,
            "Test Description");

        var insertedCapsuleId = await _mediator.Send(createCommand);

        // Act
        var response = await client.GetAsync($"api/Capsules/{insertedCapsuleId}");

        // Assert
        response.EnsureSuccessStatusCode();

        var retrievedCapsule = await response.Content.ReadFromJsonAsync<CapsuleDto>();
        Assert.NotNull(retrievedCapsule);
        Assert.Equal(1, retrievedCapsule!.Id);
        Assert.Equal(createCommand.CapsuleName, retrievedCapsule!.Name);
        Assert.Equal(createCommand.Size.ToString(), retrievedCapsule!.Size);
        Assert.Equal(createCommand.Country, retrievedCapsule!.Country);
        Assert.Equal(createCommand.Description, retrievedCapsule!.Description);
    }
    
    [Fact]
    public async Task GetCapsuleById_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        const int entityId = 1;
        var expectedException = new NotFoundException(nameof(Capsule), entityId);

        // Act
        var response = await client.GetAsync($"api/Capsules/{entityId}");

        // Assert
        await response!.AssertProblemDetailsException(expectedException);
    }
    
    [Fact]
    public async Task CreateCapsule_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        const int entityId = 0;
        var expectedException = new NotFoundException(nameof(Category), entityId);

        var createCommand = new CreateCapsuleCommand(
            "Test_Capsule",
            "Portugal",
            entityId,
            Size.Espresso,
            "Test Description");

        // Act
        var response = await client.PostAsJsonAsync("api/Capsules", createCommand);

        // Assert
        await response!.AssertProblemDetailsException(expectedException);
    }
}

public static class ExceptionAssertionExtensions
{
    public static async Task AssertProblemDetailsException(
        this HttpResponseMessage response,
        NotFoundException exception)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problemDetails);
        Assert.Equal(HttpStatusCode.NotFound.ToString(), problemDetails!.Title);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal((int)HttpStatusCode.NotFound, problemDetails.Status);
        Assert.Equal(exception.Message, problemDetails!.Detail);
    }
}