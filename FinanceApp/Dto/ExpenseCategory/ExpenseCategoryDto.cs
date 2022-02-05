using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dto.ExpenseCategory;

public class ExpenseCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

