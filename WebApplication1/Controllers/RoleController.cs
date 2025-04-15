using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repos;
using WebApplication1.DTO;
using WebApplication1.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]

public class RoleController : ControllerBase
{
    private readonly IRoleRepository _roleRepository;
    private readonly IRoleRepository _roleService;
    public RoleController(IRoleRepository rolRepository,IRoleRepository roleService)
    {
        _roleRepository = rolRepository;
        _roleService =roleService;
    }

    // [HttpPost]
    // public IActionResult AddRol(Role role)
    // {
    //     _roleRepository.AddRole(role);
    //     return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + role.IdRole, role);
    // }
    [Authorize]
    [HttpGet]
    public IActionResult GetAllRoles()
    {
        var roles = _roleService.GetRoles();
        return Ok(roles);
    }

    [HttpPost]
    public IActionResult AddRole([FromBody] RoleDTO roleDto)
    {
        var role = new Role
        {
            Name = roleDto.Name
        };
        _roleService.AddRole(role);
        return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + roleDto.IdRole, roleDto);
    }

    [HttpPut]
    public IActionResult UpdateRole([FromBody] RoleDTO role)
    {
        if (role == null)
        {
            return BadRequest(new { message = "Invalid role data." });
        }
        var existingRole = _roleService.GetRole(role.IdRole);   
        if (existingRole == null)
        {
            return NotFound(new { message = "Role not found." });
        }
        existingRole.Name = role.Name; // Update the properties as needed

        _roleService.UpdateRole(existingRole); // Update the role in the repository
        return Ok(new { message = "Role updated successfully" });
    }

    [HttpDelete]
    public IActionResult DeleteRole(int id)
    {
        _roleService.DeleteRole(id);
        return Ok(new { message = "Role deleted successfully" });
    }

}
