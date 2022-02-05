using AutoMapper;
using FinanceApp.Dto.User;
using FinanceApp.Entities;

namespace FinanceApp.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAll();
    UserDto? GetById(Guid id);
    Guid Create(CreateUserDto createUserDto);
    bool Update(Guid id, UpdateUserDto updateUserDto);
    bool Delete(Guid id);
}

public class UserService : IUserService
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IMapper _mapper;
    public UserService(FinanceAppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<UserDto> GetAll()
    {
        var users = _dbContext
            .Users
            .ToList();

        var result = _mapper.Map<List<UserDto>>(users);
        return result;
    }

    public UserDto? GetById(Guid id)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(r => r.Id == id);

        if (user is null) return null;

        var result = _mapper.Map<UserDto>(user);
        return result;
    }

    public Guid Create(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user.Id;
    }

    public bool Update(Guid id, UpdateUserDto updateUserDto)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(r => r.Id == id);

        if (user is null) return false;

        user.Username = updateUserDto.Username;
        user.Password = updateUserDto.Password;
        _dbContext.SaveChanges();
        return true;
    }

    public bool Delete(Guid id)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(r => r.Id == id);

        if(user is null) return false;

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        return true;
    }
}

