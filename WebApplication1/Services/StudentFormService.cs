using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using WebApplication1.DTO;
using WebApplication1.Models;
using WebApplication1.Repos; 



namespace WebApplication1.Services;

public class StudentFormService : IStudentFormService
{       private readonly IStudentFormRepository _studentFormRepository;
        private readonly IUserRepository _userRepository;

        public StudentFormService(IStudentFormRepository studentFormRepository, IUserRepository userRepository)
        {
            _studentFormRepository = studentFormRepository;
            _userRepository = userRepository;
        }
        
        public void AddForm(StudentFormDTO formDTO)
        {
            var user = _userRepository.GetUserByEmail(formDTO.UserEmail);
                        if (formDTO == null)
            {
                throw new ArgumentNullException(nameof(formDTO), "Form data is null");
            }


            var studentForm = new StudentForm
            {
                Nume =formDTO.Nume,
                Prenume = formDTO.Prenume,
                Facultate = formDTO.Facultate,
                Motivatie = formDTO.Motivatie,
                User = user 
            };

            _studentFormRepository.AddStudentForm(studentForm);
        }

        public void DeleteStudentForm(int idStudentForm)
        {
            var studentForm = _studentFormRepository.GetStudentForm(idStudentForm);
            if (studentForm == null)
            {
                throw new Exception("Student form not found.");
            }

            _studentFormRepository.DeleteStudentForm(idStudentForm);
        }

        public List<StudentFormDTO> GetAllForms()
        {
            var studentForms = _studentFormRepository.GetStudentForms();
            var studentFormDTOs = new List<StudentFormDTO>();

            foreach (var form in studentForms)
            {
                studentFormDTOs.Add(new StudentFormDTO
                {
                    IdStudentForm = form.IdStudentForm,
                    Nume =form.Nume,
                    Prenume =form.Prenume,
                    Facultate=form.Facultate,
                    Motivatie=form.Motivatie,
                });
            }

            return studentFormDTOs;
        }

        public List<StudentFormDTO> GetFormsByEmail(string email)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var studentForms = _studentFormRepository.GetStudentFormsByUserEmail(email);
            var studentFormDTOs = new List<StudentFormDTO>();

            foreach (var form in studentForms)
            {
                studentFormDTOs.Add(new StudentFormDTO
                {
                    IdStudentForm = form.IdStudentForm,
                    Nume =form.Nume,
                    Prenume =form.Prenume,
                    Facultate=form.Facultate,
                    Motivatie=form.Motivatie
                });
            }

            return studentFormDTOs;
        }

        public StudentFormDTO GetStudentForm(int idStudentForm)
        {
            var studentForm = _studentFormRepository.GetStudentForm(idStudentForm);
            if (studentForm == null)
            {
                throw new Exception("Student form not found.");
            }

            return new StudentFormDTO
            {
                IdStudentForm = studentForm.IdStudentForm,
                Nume = studentForm.Nume,
                Prenume = studentForm.Prenume,
                Facultate = studentForm.Facultate,
                Motivatie = studentForm.Motivatie,
            };
        }

        public void UpdateStudentForm(StudentFormDTO studentFormDTO)
        {
            var studentForm = _studentFormRepository.GetStudentForm(studentFormDTO.IdStudentForm);
            if (studentForm == null)
            {
                throw new Exception("Student form not found.");
            }

            studentForm.Nume = studentFormDTO.Nume;
            studentForm.Prenume = studentFormDTO.Prenume;
            studentForm.Facultate = studentFormDTO.Facultate;
            studentForm.Motivatie = studentFormDTO.Motivatie;

            _studentFormRepository.UpdateStudentForm(studentForm);
        }
}
