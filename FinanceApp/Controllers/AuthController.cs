using FinanceApp.Dto.Login;
using FinanceApp.Entities;
using FinanceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto loginDto)
    {
        string token = _authService.GenerateJwt(loginDto);
        return Ok(token);
    }
}

