using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Entities;

public class User
{
    public Guid Id { get; set; }

    [MaxLength(25)]
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public virtual List<Expense> Expenses { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
}