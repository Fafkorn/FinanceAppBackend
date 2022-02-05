using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dto.Expense;

public class CreateExpenseDto
{
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    [Required]
    public double Amount { get; set; }

    public DateTime CreatedTime { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ExpenseCategoryId { get; set; }
}

