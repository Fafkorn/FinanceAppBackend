using AutoMapper;
using FinanceApp.Dto.ExpenseCategory;
using FinanceApp.Entities;

namespace FinanceApp.Services;

public interface IExpenseCategoryService
{
    IEnumerable<ExpenseCategoryDto> GetAll();
    ExpenseCategoryDto? GetById(Guid id);
    Guid Create(CreateExpenseCategoryDto createExpenseCategoryDto);
    bool Update(Guid id, UpdateExpenseCategoryDto updateExpenseCategoryDto);
    bool Delete(Guid id);
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

    public ExpenseCategoryDto? GetById(Guid id)
    {
        var expenseCategory = _dbContext
            .ExpenseCategories
            .FirstOrDefault(r => r.Id == id);

        if (expenseCategory is null) return null;

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

    public bool Update(Guid id, UpdateExpenseCategoryDto updateExpenseCategoryDto)
    {
        var expenseCategory = _dbContext
            .ExpenseCategories
            .FirstOrDefault(r => r.Id == id);

        if (expenseCategory is null) return false;

        //
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Guid id)
    {
        var expenseCategory = _dbContext
            .ExpenseCategories
            .FirstOrDefault(r => r.Id == id);

        if (expenseCategory is null) return false;

        _dbContext.ExpenseCategories.Remove(expenseCategory);
        _dbContext.SaveChanges();

        return true;
    }
}
