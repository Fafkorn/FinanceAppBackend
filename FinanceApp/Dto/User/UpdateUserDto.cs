using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Dto.User;

public class UpdateUserDto
{
    [MaxLength(25)]
    public string Username { get; set; }
    public string Password { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}

