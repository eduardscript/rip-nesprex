using Application.Features.Capsule.Commands.CreateCapsule;
using Application.Features.Capsule.Commands.DeleteCapsule;
using Application.Features.Capsule.Commands.UpdateCapsule;
using Application.Features.Capsule.Queries.GetCapsuleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CapsulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CapsulesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCapsuleById([FromRoute] int id)
    {
        var category = await _mediator.Send(new GetCapsuleByIdQuery(id));

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCapsule([FromBody] CreateCapsuleCommand command)
    {
        var capsuleId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetCapsuleById), new { id = capsuleId }, value: null);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCapsule([FromRoute] int id, [FromBody] UpdateCapsuleByIdCommand command)
    {
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCapsule([FromRoute] int id)
    {
        await _mediator.Send(new DeleteCapsuleByIdCommand(id));

        return NoContent();
    }
}