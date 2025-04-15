import React, { useEffect, useState } from 'react';

const CompletedPage = () => {
  const [pdfs, setPdfs] = useState([]);
  const [loading, setLoading] = useState(true);

  const storedUser = localStorage.getItem("user");
  const email = storedUser ? JSON.parse(storedUser).email : "";
  const [pdfUrl, setPdfUrl] = useState(null);

  
  useEffect(() => {
    const fetchPdfs = async () => {
      try {
        const response = await fetch(`http://localhost:5042/api/Pdf/GetGeneratedPdfsByEmail?email=${encodeURIComponent(email)}`);

        if (response.ok) {
          const data = await response.json();
          setPdfs(data);
        }
      } catch (error) {
        console.error("Eroare la obținerea fișierelor PDF:", error);
        alert("A apărut o eroare la conectarea cu serverul.");
      } finally {
        setLoading(false);
      }
    };

    if (email) fetchPdfs();
  }, [email]);

  const handleDownloadPdf = async (url, filename) => {
    try {
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/pdf',
        }
      });

      if (!response.ok) throw new Error('Eroare la descărcarea PDF-ului');

      const blob = await response.blob();
      const downloadUrl = window.URL.createObjectURL(blob);

      const a = document.createElement('a');
      a.href = downloadUrl;
      a.download = filename;
      document.body.appendChild(a);
      a.click();
      a.remove();
      window.URL.revokeObjectURL(downloadUrl);
    } catch (err) {
      console.error(err);
      alert('Nu s-a putut descărca PDF-ul.');
    }
  };

  return (
    <div className="container mt-5">
      {loading ? (
        <p>Se încarcă lista de PDF-uri...</p>
      ) : (
        <>
          <h3>Lista de PDF-uri generate:</h3>
          <ul>
            {pdfs.length > 0 ? (
              pdfs.map((pdf, index) => (
                <li key={index}>
                <a href={`http://localhost:5042/pdfs/${encodeURIComponent(email)}/${pdf}`} target="_blank" rel="noopener noreferrer">
                {pdf}
                  </a>
                </li>
              ))
            ) : (
              <p>Nu există fișiere PDF generate.</p>
            )}
          </ul>
          {pdfUrl && (
            <div className="text-center mt-3">
              {/* <button onClick={handleDownloadPdf} className="btn btn-success"> */}
              <button className="btn btn-success">
                Descarcă PDF-ul
              </button>
            </div>
          )}
        </>
      )}
    </div>
  );
};

export default CompletedPage;
