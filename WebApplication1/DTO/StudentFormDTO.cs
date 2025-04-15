using System;

namespace WebApplication1.DTO;

public class StudentFormDTO
{
    public int IdStudentForm { get; set; }
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public string Facultate { get; set; }
    public string Motivatie { get; set; }

    public string UserEmail { get; set; } // Navigabilitate către User

}
