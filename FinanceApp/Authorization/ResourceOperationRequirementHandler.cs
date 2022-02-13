using FinanceApp.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FinanceApp.Authorization;

public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Expense>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        ResourceOperationRequirement requirement, Expense expense)
    {
        if(requirement.ResourceOperation == ResourceOperation.Read ||
            requirement.ResourceOperation == ResourceOperation.Create)
        {
            context.Succeed(requirement);
        }

        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        if(expense.UserId.ToString() == userId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
