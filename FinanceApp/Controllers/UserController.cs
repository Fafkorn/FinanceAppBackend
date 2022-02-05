using FinanceApp.Dto.User;
using FinanceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<UserDto>> GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public ActionResult<UserDto> Get([FromRoute] Guid id)
    {
        var user = _userService.GetById(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult Create([FromBody] CreateUserDto createUserDto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = _userService.Create(createUserDto);
        return Created($"/api/user/{id}", null);
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var isUpdated = _userService.Update(id, updateUserDto);
        if(!isUpdated)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        var isDeleted = _userService.Delete(id);

        if(isDeleted)
        {
            return NoContent();
        }
        return NotFound();
    }
}

