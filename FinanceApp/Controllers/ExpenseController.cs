using FinanceApp.Dto.Expense;
using FinanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
        return Ok(expense);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateExpenseDto createExpenseDto)
    {
        var id = _expenseService.Create(createExpenseDto);
        return Created($"/api/expense/{id}", null);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] Guid id, [FromBody] UpdateExpenseDto updateExpenseDto)
    {
        _expenseService.Update(id, updateExpenseDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        _expenseService.Delete(id);
        return NoContent();
    }
}

