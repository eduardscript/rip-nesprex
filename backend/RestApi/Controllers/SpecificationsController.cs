using Application.Features.Specification.Queries;
using Application.Features.Specification.Queries.GetAllSpecifications;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SpecificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{size}")]
    public async Task<IActionResult> GetSpecificationByName([FromRoute] string size)
    {
        var specification = await _mediator.Send(new GetSpecificationBySizeQuery(size));

        return Ok(specification);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSpecifications()
    {
        var specifications = await _mediator.Send(new GetAllSpecificationsQuery());

        return Ok(specifications);
    }
}