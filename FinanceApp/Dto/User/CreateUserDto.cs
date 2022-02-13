using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dto.User;

public class CreateUserDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
}

