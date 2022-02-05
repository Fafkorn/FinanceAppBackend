using AutoMapper;
using FinanceApp.Dto.Expense;
using FinanceApp.Entities;

namespace FinanceApp.Services;

public interface IExpenseService
{
    IEnumerable<ExpenseDto> GetAll();
    ExpenseDto? GetById(Guid id);
    Guid Create(CreateExpenseDto createExpenseDto);
    bool Update(Guid id, UpdateExpenseDto updateExpenseDto);
    bool Delete(Guid id);
}
public class ExpenseService : IExpenseService
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseService(FinanceAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public IEnumerable<ExpenseDto> GetAll()
    {
        var expenses = _dbContext
            .Expenses
            .ToList();

        var result = _mapper.Map<List<ExpenseDto>>(expenses);
        return result;
    }

    public ExpenseDto? GetById(Guid id)
    {
        var expense = _dbContext
           .Expenses
           .FirstOrDefault(r => r.Id == id);

        if (expense is null) return null;

        var result = _mapper.Map<ExpenseDto>(expense);
        return result;
    }

    public Guid Create(CreateExpenseDto createExpenseDto)
    {
        var expense = _mapper.Map<Expense>(createExpenseDto);
        _dbContext.Expenses.Add(expense);
        _dbContext.SaveChanges();
        return expense.Id;
    }

    public bool Update(Guid id, UpdateExpenseDto updateExpenseDto)
    {
        var expense = _dbContext
            .Expenses
            .FirstOrDefault(r => r.Id == id);

        if (expense is null) return false;

        //
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Guid id)
    {
        var expense = _dbContext
            .Expenses
            .FirstOrDefault(r => r.Id == id);

        if (expense is null) return false;

        _dbContext.Expenses.Remove(expense);
        _dbContext.SaveChanges();

        return true;
    }
}

