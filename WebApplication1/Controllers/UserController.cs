using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repos;
using WebApplication1.Services;
using WebApplication1.DTO;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController :ControllerBase
{
    private readonly IUserService _userService;
    private readonly AppDbContext _context; // Contextul pentru accesarea bazei de date
    // Constructorul pentru injectarea serviciului
    public UserController(IUserService userService, AppDbContext context)
    {
        _context = context; // Inițializăm contextul pentru a accesa baza de date
        _userService = userService; // Inițializăm serviciul utilizator
    }
     

    [HttpPost("user")]
    public IActionResult AddUser([FromBody] UserDTO userDTO)
    {
         
        _userService.AddUser(userDTO); // totul se face în service
        return Ok(new { message = "User created successfully" });
    }
    // [HttpPost("create")]
    // public IActionResult CreateUser([FromBody] UserDTO userDTO)
    // {
    //     try
    //     {
    //         _userService.AddUser(userDTO);
    //         return Ok("User created successfully");
    //     }
    //     catch (Exception ex)
    //     {
    //         return BadRequest(ex.Message);
    //     }
    // }
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _userService.GetAllUsers(); // Service returnează UserDTO
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return Ok(new { message = "User deleted successfully" });
    }

    [HttpPut]
    public IActionResult UpdateUser([FromBody] UserDTO userDTO)
    {
        _userService.UpdateUser(userDTO);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpGet("{userId}/roles")]
    public IActionResult GetUserRoles(int userId)
    {
        var roles = _userService.GetUserRoleById(userId);
        return Ok(roles);
    }

}
