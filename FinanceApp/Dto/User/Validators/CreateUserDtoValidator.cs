using FluentValidation;

namespace FinanceApp.Dto.User.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{

    public CreateUserDtoValidator(FinanceAppDbContext dbContext)
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password).MinimumLength(6);

        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

        RuleFor(x => x.Username)
            .Custom((value, context) =>
            {
                var isUsernameInUse = dbContext.Users.Any(u => u.Username == value);
                if (isUsernameInUse)
                {
                    context.AddFailure("Username", "That username is already taken");
                }
            });

        RuleFor(x => x.Email)
            .Custom((value, context) =>
            {
                var isEmailInUse = dbContext.Users.Any(u => u.Email == value);
                if(isEmailInUse)
                {
                    context.AddFailure("Email", "That email address is already taken");
                }
            });

        RuleFor(x => x.RoleId)
            .Custom((value, context) =>
            {
                var roleExisits = dbContext.Roles.Any(r => r.Id == value);
                if(!roleExisits)
                {
                    context.AddFailure("RoleId", "Role doesn't exist");
                }
            });
    }
}

