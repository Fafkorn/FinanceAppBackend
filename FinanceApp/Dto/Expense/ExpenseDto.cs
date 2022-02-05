namespace FinanceApp.Dto.Expense;

public class ExpenseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTime { get; set; }
    public double Amount { get; set; }

}

