using AutoMapper;
using FinanceApp.Authorization;
using FinanceApp.Dto.Expense;
using FinanceApp.Entities;
using FinanceApp.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinanceApp.Services;

public interface IExpenseService
{
    IEnumerable<ExpenseDto> GetAll();
    ExpenseDto GetById(Guid id);
    Guid Create(CreateExpenseDto createExpenseDto);
    void Update(Guid id, UpdateExpenseDto updateExpenseDto);
    void Delete(Guid id);
}
public class ExpenseService : IExpenseService
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextService _userContextService;

    public ExpenseService(FinanceAppDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _authorizationService = authorizationService;
        _userContextService = userContextService;
    }
    public IEnumerable<ExpenseDto> GetAll()
    {
        var expenses = _dbContext
            .Expenses
            .ToList();

        var result = _mapper.Map<List<ExpenseDto>>(expenses);
        return result;
    }

    public ExpenseDto GetById(Guid id)
    {
        var expense = _dbContext
           .Expenses
           .FirstOrDefault(r => r.Id == id);

        if (expense is null)
            throw new NotFoundException("Expense not found");

        var result = _mapper.Map<ExpenseDto>(expense);
        return result;
    }

    public Guid Create(CreateExpenseDto createExpenseDto)
    {
        var expense = _mapper.Map<Expense>(createExpenseDto);

        expense.UserId = _userContextService.GetUserId.Value;
        _dbContext.Expenses.Add(expense);
        _dbContext.SaveChanges();
        return expense.Id;
    }

    public void Update(Guid id, UpdateExpenseDto updateExpenseDto)
    {
        var expense = _dbContext
            .Expenses
            .FirstOrDefault(r => r.Id == id);

        if (expense is null)
            throw new NotFoundException("Expense not found");

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, expense, 
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;

        if(!authorizationResult.Succeeded)
        {
            throw new ForbiddenException("");
        }
        //TODO update
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var expense = _dbContext
            .Expenses
            .FirstOrDefault(r => r.Id == id);

        if (expense is null)
            throw new NotFoundException("Expense not found");

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, expense,
            new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

        _dbContext.Expenses.Remove(expense);
        _dbContext.SaveChanges();
    }
}

