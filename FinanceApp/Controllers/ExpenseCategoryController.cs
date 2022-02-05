using FinanceApp.Dto.ExpenseCategory;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseCategoryController : ControllerBase
{
    private readonly IExpenseCategoryService _expenseCategoryService;

    public ExpenseCategoryController(IExpenseCategoryService expenseCategoryService)
    {
        _expenseCategoryService = expenseCategoryService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ExpenseCategoryDto>> GetAll()
    {
        var expenseCategories = _expenseCategoryService.GetAll();
        return Ok(expenseCategories);
    }

    [HttpGet("{id}")]
    public ActionResult<ExpenseCategoryDto> Get([FromRoute] Guid id)
    {
        var expenseCategory = _expenseCategoryService.GetById(id);

        if (expenseCategory is null)
        {
            return NotFound();
        }

        return Ok(expenseCategory);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateExpenseCategoryDto createExpenseCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = _expenseCategoryService.Create(createExpenseCategoryDto);
        return Created($"/api/expenseCategory/{id}", null);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] Guid id, [FromBody] UpdateExpenseCategoryDto updateExpenseCategoryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var isUpdated = _expenseCategoryService.Update(id, updateExpenseCategoryDto);
        if (!isUpdated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        var isDeleted = _expenseCategoryService.Delete(id);

        if (isDeleted)
        {
            return NoContent();
        }
        return NotFound();
    }
}
