using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PdfController : ControllerBase
    {
        private readonly PdfGenerationService _pdfService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PdfController(PdfGenerationService pdfService, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
          _pdfService = pdfService;
        }

        [HttpPost]
        public IActionResult GeneratePdf([FromForm] string studentName, [FromForm] string studentPrenume, [FromForm] string facultate, [FromForm] string motivatie)
        {
            var fileName = $"StudentForm_{studentName}_{DateTime.Now:yyyyMMdd_HHmmss}";
            var pdfPath = _pdfService.GenerateStudentPdf(studentName, studentPrenume, facultate, motivatie);
            // var fileBytes = System.IO.File.ReadAllBytes(pdfPath);
            return File(pdfPath, "application/pdf", "formular_student.pdf");

            // var pdfBytes = _pdfService.GenerateStudentPdf(model.Nume, model.Prenume, model.Facultate, model.Motivatie);
            // var fileName = $"fisa_{model.Nume}_{model.Prenume}.pdf";

            // return File(pdfBytes, "application/pdf", fileName);
        }

        // Endpoint pentru a obține lista de PDF-uri
        [HttpGet]
        public IActionResult GetGeneratedPdfs()
        {
            string pdfDirectory = Path.Combine(_webHostEnvironment.ContentRootPath, "pdfs");

            if (!Directory.Exists(pdfDirectory))
            {
                return NotFound("No PDFs found.");
            }

            var pdfFiles = Directory.GetFiles(pdfDirectory, "*.pdf")
                                    .Select(file => Path.GetFileName(file))
                                    .ToList();

            // Returnează lista de PDF-uri
            return Ok(pdfFiles);
        }

        [HttpGet]
        public IActionResult GetGeneratedPdfsByEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
            return BadRequest("Email-ul este necesar.");
            }

        // Căutăm fișierele PDF în directorul corespunzător email-ului
        var emailDirectory = Path.Combine(_webHostEnvironment.ContentRootPath, "pdfs", email);
        
        // Verificăm dacă există fișiere pentru acel email
        if (!Directory.Exists(emailDirectory))
        {
            return NotFound("Nu au fost găsite PDF-uri pentru acest utilizator.");
        }

        // Obținem lista de fișiere PDF din directorul respectiv
        var pdfFiles = Directory.GetFiles(emailDirectory);
        
        // Extragem doar numele fișierului
        var pdfs = pdfFiles.Select(file => Path.GetFileName(file)).ToList();

        // Returnăm lista cu fișierele PDF găsite
        return Ok(pdfs);
        }
       [HttpGet]
        public IActionResult DownloadPdf(string fileName)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "pdfs", fileName);

            // Verificăm dacă fișierul există
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Fișierul nu a fost găsit.");
            }

            // Returnăm fișierul ca fișier descărcabil
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var contentType = "application/pdf";
            return File(fileBytes, contentType, fileName);
        }

    }
}
