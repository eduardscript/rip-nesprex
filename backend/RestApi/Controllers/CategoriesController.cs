using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Queries.GetAllCategories;
using Application.Features.Category.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        var category = await _mediator.Send(new GetCategoryByIdQuery(id));

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        var categoryId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetCategoryById), new { id = categoryId }, value: null);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await _mediator.Send(new DeleteCategoryCommand(id));

        return NoContent();
    }
}