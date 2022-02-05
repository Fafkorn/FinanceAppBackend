using FinanceApp.Dto.Expense;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ExpenseDto>> GetAll()
    {
        var expenses = _expenseService.GetAll();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public ActionResult<ExpenseDto> Get([FromRoute] Guid id)
    {
        var expense = _expenseService.GetById(id);

        if (expense is null)
        {
            return NotFound();
        }

        return Ok(expense);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateExpenseDto createExpenseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = _expenseService.Create(createExpenseDto);
        return Created($"/api/expense/{id}", null);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] Guid id, [FromBody] UpdateExpenseDto updateExpenseDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var isUpdated = _expenseService.Update(id, updateExpenseDto);
        if (!isUpdated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        var isDeleted = _expenseService.Delete(id);

        if (isDeleted)
        {
            return NoContent();
        }
        return NotFound();
    }
}

