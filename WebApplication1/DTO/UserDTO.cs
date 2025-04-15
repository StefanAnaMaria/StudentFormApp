using System;
using WebApplication1.Models;

namespace WebApplication1.DTO;

public class UserDTO
{
    public int IdUser { get; set; }
    public string Name { get; set; }
    public string Email { get; set; } 
    public string Password { get; set; } 

    // public int IdRole{ get; set; }
     public string? RoleName { get; set; } // Numele rolului asociat utilizatorului
    public int RoleId { get; set; } // ID-ul rolului asociat utilizatorului

}
