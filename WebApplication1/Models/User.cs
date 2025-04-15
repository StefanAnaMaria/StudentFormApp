using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebApplication1.Models;
[PrimaryKey(propertyName: "IdUser")]

public class User
{
    public int IdUser { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role? Role { get; set; } = new Role(); // Navigabilitate către rol

    public List<StudentForm>? ListaStudentForms { get; set; }
}