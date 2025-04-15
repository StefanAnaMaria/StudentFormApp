using System;
using WebApplication1.Models;

namespace WebApplication1.Repos;

public class SqlStudentFormRepository : IStudentFormRepository
{
    private readonly AppDbContext _context;
    public SqlStudentFormRepository(AppDbContext context)
    {
        _context = context;
    }
    public void AddStudentForm(StudentForm studentForm)
    {
        _context.StudentForms.Add(studentForm);
        _context.SaveChanges();
    }

    public void DeleteStudentForm(int idStudentForm)
    {
       var studentFormDeModificat = _context.StudentForms.Find(idStudentForm);
        if (studentFormDeModificat != null)
        {
            _context.StudentForms.Remove(studentFormDeModificat);
            _context.SaveChanges();
        }
    }

    public StudentForm GetStudentForm(int idStudentForm)
    {
        var studentForm = _context.StudentForms.Find(idStudentForm);
        if (studentForm != null)
        {
            return studentForm;
        }
        return null;
    }

    public List<StudentForm> GetStudentForms()
    {
        var rez = _context.StudentForms.ToList();
        return rez;
    }

    public List<StudentForm> GetStudentFormsByUserEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            return _context.StudentForms.Where(sf => sf.User.IdUser == user.IdUser).ToList();
        }
        return new List<StudentForm>();
    }

    public void UpdateStudentForm(StudentForm studentForm)
    {
        var studentFormDeModificat = _context.StudentForms.Find(studentForm.IdStudentForm);
        if( studentFormDeModificat != null)
        {
            studentFormDeModificat.Nume = studentForm.Nume;
            studentFormDeModificat.Prenume = studentForm.Prenume;
            studentFormDeModificat.Facultate = studentForm.Facultate;
            studentFormDeModificat.Motivatie = studentForm.Motivatie;
            _context.SaveChanges();
        }
    }
    public StudentForm GetStudentFormById(int id)
    {
        // Caută formularul de student după ID
        var studentForm = _context.StudentForms.Find(id);
        if (studentForm != null)
        {
            return studentForm;
        }
        return null;
    }
}
