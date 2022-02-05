using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Entities;

public class User
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(25)]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public virtual List<Expense> Expenses { get; set; }
}