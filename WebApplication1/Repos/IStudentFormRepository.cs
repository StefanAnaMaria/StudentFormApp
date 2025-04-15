using System;
using WebApplication1.Models;
namespace WebApplication1.Repos;

public interface IStudentFormRepository
{

    public void AddStudentForm(StudentForm studentForm);    
    public void DeleteStudentForm(int idStudentForm);
    public void UpdateStudentForm(StudentForm studentForm);
    public StudentForm GetStudentForm(int idStudentForm);
    public List<StudentForm> GetStudentForms(); 
    public List<StudentForm> GetStudentFormsByUserEmail(string email);

}
