using FinanceApp.Entities;

namespace FinanceApp;

public class FinanceAppSeeder
{
    private readonly FinanceAppDbContext _dbContext;

    public FinanceAppSeeder(FinanceAppDbContext dbContext)
    {
        _dbContext = dbContext;
        Seed();
    }

    public void Seed()
    {
        if(_dbContext.Database.CanConnect())
        {
            if(!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>()
        {
            new Role()
            {
                Id = new Guid("fab2c13f-f950-4977-a935-8e14e8dbac20"),
                Name = "Administrator"
            },
            new Role()
            {
                Id = new Guid("0758e8d0-90cc-42f9-a49a-baf74ba3da15"),
                Name = "User"
            }
        };
        return roles;
    }
}

