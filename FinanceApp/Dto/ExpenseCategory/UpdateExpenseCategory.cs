using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dto.ExpenseCategory;

public class UpdateExpenseCategoryDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}

