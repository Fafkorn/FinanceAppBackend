using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Entities;

public class Expense
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime CreatedTime { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    [Required]
    public Guid ExpenseCategoryId { get; set; }
    public virtual ExpenseCategory ExpenseCategory { get; set; }
}

