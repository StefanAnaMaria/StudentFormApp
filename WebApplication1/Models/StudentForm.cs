using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models;

public class StudentForm
{
    [Key]
    public int IdStudentForm { get; set; }
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public string Facultate { get; set; }
    public string Motivatie { get; set; }
    public DateTime DataSubmisiei { get; set; }

    // Legătura cu User (studentul care a completat formularul)
    public User User { get; set; } // Navigabilitate către User
    public StudentForm()
    {
        DataSubmisiei = DateTime.Now;
    }

}
