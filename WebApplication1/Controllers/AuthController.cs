using System;
using Microsoft.AspNetCore.Mvc; 
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/autentificare")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtService _jwtService;
    public AuthController(JwtService jwtService, IUserService userService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel login)
    {
        var user = _userService.GetUserByEmail (login.Email);

        if (user == null)
            return Unauthorized(new { message = "Email sau parolă incorectă." });

        var roles = _userService.GetUserRoleById(user.IdUser);
        if (roles == null || !roles.Any())
             return Unauthorized(new { message = "Utilizatorul nu are niciun rol." });

        var roleNames = roles.Select(r => r.Name).ToList();
        var token = _jwtService.GenerateSecurityToken(user.Email, roleNames);

        return Ok(new
        {
            Token = token,
            Email = user.Email,
            Roles = roles
        });
    }
    
}


