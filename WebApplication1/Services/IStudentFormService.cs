using System;
using WebApplication1.DTO;
using WebApplication1.Models;
namespace WebApplication1.Services;

public interface IStudentFormService
{
    public void AddForm(StudentFormDTO form);
    public StudentFormDTO GetStudentForm(int idStudentForm);

    public List<StudentFormDTO> GetAllForms();
    public List<StudentFormDTO> GetFormsByEmail(string email);
    public void DeleteStudentForm(int idStudentForm);
    public void UpdateStudentForm(StudentFormDTO studentForm);


}
