using AutoMapper;
using FinanceApp.Dto.User;
using FinanceApp.Entities;
using FinanceApp.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Services;

public interface IUserService
{
    IEnumerable<UserDto> GetAll();
    UserDto GetById(Guid id);
    Guid Create(CreateUserDto createUserDto);
    void Update(Guid id, UpdateUserDto updateUserDto);
    void Delete(Guid id);
}

public class UserService : IUserService
{
    private readonly FinanceAppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(FinanceAppDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public IEnumerable<UserDto> GetAll()
    {
        var users = _dbContext
            .Users
            .ToList();

        var result = _mapper.Map<List<UserDto>>(users);
        return result;
    }

    public UserDto GetById(Guid id)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(r => r.Id == id);

        if (user is null)
            throw new NotFoundException("User not found");

        var result = _mapper.Map<UserDto>(user);
        return result;
    }

    public Guid Create(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);
        var hashedPassword =  _passwordHasher.HashPassword(user, createUserDto.Password);
        user.Password = hashedPassword;
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user.Id;
    }

    public void Update(Guid id, UpdateUserDto updateUserDto)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(r => r.Id == id);

        if (user is null)
            throw new NotFoundException("User not found");

        user.Username = updateUserDto.Username;
        var hashedPassword = _passwordHasher.HashPassword(user, updateUserDto.Password);
        user.Password = hashedPassword;
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var user = _dbContext
            .Users
            .FirstOrDefault(r => r.Id == id);

        if(user is null)
            throw new NotFoundException("User not found");

        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();
    }
}

