using Companio.DTO;
using Companio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Companio.Controllers;

[ApiController]
[Route("api/v1")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDTO userDto)
    {
        IActionResult response = Unauthorized();
        var user = _authService.AuthenticateUser(userDto);

        if (user != null)
        {
            var tokenString = _tokenService.GenerateJWTToken(user);
            response = Ok(new { token = tokenString });
        }

        return response;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserDTO userDto)
    {
        if (_authService.IsEmailAvailable(userDto.Email))
        {
            var user = _authService.RegisterUser(userDto);
            var tokenString = _tokenService.GenerateJWTToken(user);
            return Ok(new { token = tokenString });
        }

        return BadRequest("Email is already taken");
    }
}