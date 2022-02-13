using FinanceApp.Dto.ExpenseCategory;
using FinanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
        return Ok(expenseCategory);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateExpenseCategoryDto createExpenseCategoryDto)
    {
        var id = _expenseCategoryService.Create(createExpenseCategoryDto);
        return Created($"/api/expenseCategory/{id}", null);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] Guid id, [FromBody] UpdateExpenseCategoryDto updateExpenseCategoryDto)
    {
        _expenseCategoryService.Update(id, updateExpenseCategoryDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        _expenseCategoryService.Delete(id);
        return NoContent();
    }
}
