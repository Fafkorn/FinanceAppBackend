using FinanceApp.Dto.User;
using FinanceApp.Entities;
using FinanceApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public ActionResult<IEnumerable<UserDto>> GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserDto> Get([FromRoute] Guid id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult Create([FromBody] CreateUserDto createUserDto)
    {
        var id = _userService.Create(createUserDto);
        return Created($"/api/user/{id}", null);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        _userService.Update(id, updateUserDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        _userService.Delete(id);
        return NoContent();
    }
}

