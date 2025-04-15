using System;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Models;

    [PrimaryKey(propertyName: "IdRole")]
    public class Role
{
    public int IdRole { get; set; }
    public string Name { get; set; } = string.Empty;

}
