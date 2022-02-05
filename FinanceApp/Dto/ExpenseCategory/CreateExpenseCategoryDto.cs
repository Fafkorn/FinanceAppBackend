using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dto.ExpenseCategory;

public class CreateExpenseCategoryDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}
