using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Entities;
public class ExpenseCategory
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
}
