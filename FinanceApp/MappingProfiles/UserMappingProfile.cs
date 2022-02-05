using AutoMapper;
using FinanceApp.Dto.Expense;
using FinanceApp.Dto.ExpenseCategory;
using FinanceApp.Dto.User;
using FinanceApp.Entities;

namespace FinanceApp;
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();

        CreateMap<ExpenseCategory, ExpenseCategoryDto>();
        CreateMap<CreateExpenseCategoryDto, ExpenseCategory>();

        CreateMap<Expense, ExpenseDto>();
        CreateMap<CreateExpenseDto, Expense>();
    }
}
