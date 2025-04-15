using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services; // Importăm serviciile (sau repo-ul)
using System.Collections.Generic;
using WebApplication1.DTO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element; // Importăm DTO-urile necesare
using WebApplication1.Repos;
using Microsoft.AspNetCore.Authorization; // Importăm repo-urile necesare

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentFormController : ControllerBase
    {
        private readonly IStudentFormService _studentFormService;
        private readonly IUserRepository _userRepository; 
        private readonly IStudentFormRepository _studentFormRepository;
        private readonly  AppDbContext _context;         private readonly PdfGenerationService _pdfService; 
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Iniectăm serviciul în constructor
        public StudentFormController(IStudentFormService studentFormService, IUserRepository userRepository, IStudentFormRepository studentFormRepository,
         AppDbContext context, PdfGenerationService pdfService,IWebHostEnvironment webHostEnvironment)
        {
            _studentFormService = studentFormService; // Inițializăm StudentFormService
            _userRepository = userRepository; // Inițializăm UserRepository
            _studentFormRepository = studentFormRepository; // Inițializăm StudentFormRepository
            _context = context; // Inițializăm AppDbContext
            _pdfService=pdfService;
            _webHostEnvironment = webHostEnvironment; // Inițializăm IWebHostEnvironment
        }

        [HttpGet]
        public ActionResult<List<StudentForm>> GetAllStudentForms()
        {
            var studentForms = _studentFormService.GetAllForms(); // Obținem toate formularele din serviciu

            return Ok(studentForms);
        }

        // GET: api/StudentForm/5
        [HttpGet("{id}")]
        public ActionResult<StudentForm> GetStudentForm(int id)
        {
            var studentForm = _studentFormService.GetStudentForm(id);
            if (studentForm == null)
            {
                return NotFound();
            }
            return Ok(studentForm);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentFormWithPdf([FromBody] StudentFormDTO studentFormDTO)
        {
            if (studentFormDTO == null)
                return BadRequest("Invalid form data.");

            var emailDirectory = Path.Combine(_webHostEnvironment.ContentRootPath, "pdfs", studentFormDTO.UserEmail);

            if (!Directory.Exists(emailDirectory))
            {
                Directory.CreateDirectory(emailDirectory);
            }
            
                // 1. Salvăm formularul în BD
                _studentFormService.AddForm(studentFormDTO);
            
                // 2. Generăm PDF-ul
                var pdfBytes = _pdfService.GenerateStudentPdf(
                    studentFormDTO.Nume,
                    studentFormDTO.Prenume,
                    studentFormDTO.Facultate,
                    studentFormDTO.Motivatie
                );

            var fileName = $"Fisa_{studentFormDTO.Nume}_{studentFormDTO.Prenume}_{DateTime.Now.Ticks}.pdf";
            var filePath = Path.Combine(emailDirectory, fileName);

            // Salvăm fișierul PDF pe disc
            System.IO.File.WriteAllBytes(filePath, pdfBytes);

            // Returnăm un URL către fișierul PDF generat
            var fileUrl = $"{Request.Scheme}://{Request.Host}/generated-pdfs/{fileName}";
            // , pdfName = fileName
            return Ok(new { pdfUrl = fileUrl});
        }


        // GET: api/StudentForm/by-email?email=test@example.com
        [HttpGet]
        public ActionResult<List<StudentForm>> GetStudentFormsByUserEmail(string email)
        {
            var studentForms = _studentFormService.GetFormsByEmail(email);
            if (studentForms == null || studentForms.Count == 0)
            {
                return NotFound();
            }
            return Ok(studentForms);
        }

        // POST: api/StudentForm
        [HttpPost]
        public ActionResult<StudentFormDTO> AddStudentForm([FromBody] StudentFormDTO studentFormDTO)
        {
            if (studentFormDTO == null)
            {
                return BadRequest("Invalid form data.");
            }
            _studentFormService.AddForm(studentFormDTO); // Adăugăm formularul în serviciu
            return CreatedAtAction(nameof(GetStudentForm), new { id = studentFormDTO.IdStudentForm }, studentFormDTO);
        }

        // PUT: api/StudentForm/5
        [HttpPut("{id}")]
        public ActionResult UpdateStudentForm(int id, [FromBody] StudentFormDTO studentFormDTO)
        {
            if (studentFormDTO == null)
            {
                return BadRequest("Invalid form data.");
            }

            // Verificăm dacă ID-ul din URL corespunde cu ID-ul din DTO
            if (id != studentFormDTO.IdStudentForm)
            {
                return BadRequest("Form ID mismatch.");
            }

            // Verificăm dacă formularul există
            var existingForm = _studentFormService.GetStudentForm(id);
            if (existingForm == null)
            {
                return NotFound();
            }

            // Actualizăm formularul
            _studentFormService.UpdateStudentForm(studentFormDTO);
            // return NoContent();
            return Ok(new { message = "Form updated successfully" });
        }

        // DELETE: api/StudentForm/5
        [HttpDelete("{id}")]
        public ActionResult DeleteStudentForm(int id)
        {
            var studentForm = _studentFormService.GetStudentForm(id);
            if (studentForm == null)
            {
                return NotFound();
            }

            _studentFormService.DeleteStudentForm(id);
            return NoContent();
        }
    }
}
