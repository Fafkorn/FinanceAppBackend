using AutoMapper;
using FinanceApp.Dto.ExpenseCategory;
using FinanceApp.Entities;
using FinanceApp.Exceptions;

namespace FinanceApp.Services;

public interface IExpenseCategoryService
{
    IEnumerable<ExpenseCategoryDto> GetAll();
    ExpenseCategoryDto GetById(Guid id);
    Guid Create(CreateExpenseCategoryDto createExpenseCategoryDto);
    void Update(Guid id, UpdateExpenseCategoryDto updateExpenseCategoryDto);
    void Delete(Guid id);
}
public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseCategoryService(FinanceAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<ExpenseCategoryDto> GetAll()
    {
        var expenseCategories = _dbContext
            .ExpenseCategories
            .ToList();

        var result = _mapper.Map<List<ExpenseCategoryDto>>(expenseCategories);
        return result;
    }

    public ExpenseCategoryDto GetById(Guid id)
    {
        var expenseCategory = _dbContext
            .ExpenseCategories
            .FirstOrDefault(r => r.Id == id);

        if (expenseCategory is null)
            throw new NotFoundException("Expense category not found");

        var result = _mapper.Map<ExpenseCategoryDto>(expenseCategory);
        return result;
    }

    public Guid Create(CreateExpenseCategoryDto createExpenseCategoryDto)
    {
        var expenseCategory = _mapper.Map<ExpenseCategory>(createExpenseCategoryDto);
        _dbContext.ExpenseCategories.Add(expenseCategory);
        _dbContext.SaveChanges();
        return expenseCategory.Id;
    }

    public void Update(Guid id, UpdateExpenseCategoryDto updateExpenseCategoryDto)
    {
        var expenseCategory = _dbContext
            .ExpenseCategories
            .FirstOrDefault(r => r.Id == id);

        if (expenseCategory is null)
            throw new NotFoundException("Expense category not found");

        //
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var expenseCategory = _dbContext
            .ExpenseCategories
            .FirstOrDefault(r => r.Id == id);

        if (expenseCategory is null)
            throw new NotFoundException("Expense category not found");

        _dbContext.ExpenseCategories.Remove(expenseCategory);
        _dbContext.SaveChanges();
    }
}
