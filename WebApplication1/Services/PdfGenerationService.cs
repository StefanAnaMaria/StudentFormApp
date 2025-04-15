using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Hosting;
using System.IO;



namespace WebApplication1.Services;
public class PdfGenerationService{
    private readonly IWebHostEnvironment _webHostEnvironment;
    public PdfGenerationService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public byte[] GenerateStudentPdf(string studentName, string studentPrenume, string facultate, string motivatie)
    {
    //     using var memoryStream = new MemoryStream();
    //     var writerProps = new WriterProperties();
    //     using var writer = new PdfWriter(memoryStream, writerProps);
    //     using var pdf = new PdfDocument(writer);
    //     var document = new Document(pdf);

    //     document.Add(new Paragraph($"Nume: {studentName}"));
    //     document.Add(new Paragraph($"Vârstă: {studentAge}"));
    //     document.Add(new Paragraph($"Curs: {studentCourse}"));
    //     document.Close();

    //     return memoryStream.ToArray();
    // }

    // using (var stream = new MemoryStream())
    // {
    //     var writerProperties = new WriterProperties(); // FĂRĂ .SetSmartMode
    //     var writer = new PdfWriter(stream, writerProperties);
    //     var pdf = new PdfDocument(writer);
    //     var document = new Document(pdf);

    //     document.Add(new Paragraph("Salut! Acesta este un PDF simplu."));
    //     document.Close();

    //     return stream.ToArray();
    // }}
     using (var stream = new MemoryStream())
            {
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

             document.Add(new Paragraph("Fisa Studentului")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(18)
                .SetMarginBottom(20));

                // Datele studentului
            document.Add(new Paragraph($"Nume: {studentName}"));
            document.Add(new Paragraph($"Prenume: {studentPrenume}"));
            document.Add(new Paragraph($"Facultate: {facultate}"));
            document.Add(new Paragraph($"Motivație: {motivatie}"));

            // Data generării
            document.Add(new Paragraph($"\nGenerat la data de: {DateTime.Now:dd.MM.yyyy HH:mm}"));

            // Semnătură
            document.Add(new Paragraph("\n\nSemnat electronic").SetTextAlignment(TextAlignment.RIGHT));

            document.Close();
            return stream.ToArray();
        }
    }
}
