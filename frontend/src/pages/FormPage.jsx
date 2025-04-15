import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Pagesstyle.css'; 


const FormPage = () => {
  
  const storedUser = localStorage.getItem("user");
  const email = storedUser ? JSON.parse(storedUser).email : "";


  const [formData, setFormData] = useState({
    nume: "",
    prenume: "",
    facultate: "",
    motivatie: "",
    UserEmail: email // sau "email", cum îl ai tu salvat
  });
  const [errors, setErrors] = useState({
    nume: '',
    prenume: '',
    facultate: '',
    motivatie: '',
  });
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [messageVisible, setMessageVisible] = useState(false); 
  const navigate = useNavigate(); // Asigură-te că ai importat useNavigate din react-router-dom

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
  };


  const validateForm = () => {
    const newErrors = {};

    if (!formData.nume) newErrors.nume = 'Numele este obligatoriu';
    if (!formData.prenume) newErrors.prenume = 'Prenumele este obligatoriu';
    if (!formData.facultate) newErrors.facultate = 'Facultatea este obligatorie';

    // Verifică dacă motivarea are cel puțin 100 de cuvinte
    if (!formData.motivatie) {
      newErrors.motivatie = 'Motivația este obligatorie';
    } else {
      const words = formData.motivatie.trim().split(/\s+/);
      if (words.length < 100) {
        newErrors.motivatie = 'Motivația trebuie să aibă cel puțin 100 de cuvinte';
      }
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;  // Dacă nu sunt erori, returnează true
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
  try {
    // Trimite formularul către endpoint-ul care salvează și generează PDF-ul
    const response = await fetch('http://localhost:5042/api/StudentForm/AddStudentFormWithPdf', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(formData),
    });

    if (!response.ok) {
      const message = await response.text(); // Poți salva mesajul de eroare din backend
      alert(`Eroare la trimiterea formularului:\n${message}`);
      return;
    }

    const data = await response.json();

    if (data.pdfUrl) {
      window.location.href = "/completed"; // Sau redirect oriunde vrei
    }
  } catch (err) {
    console.error(err);
    alert("Ceva n-a mers bine...");
  }

};
  

  return (
    <div className="container">
      {/* Secțiunea de text care va ocupa întreaga lățime */}
      <div className="text-section">
        <h3 className="form-description">Invitație pentru completarea formularului</h3>
        <p className="form-message">
          Acest formular este necesar pentru completarea procesului de înregistrare.
          Te rugăm să completezi toate câmpurile corect. Mulțumim că ai ales să participi!
        </p>
      </div>

      {/* Secțiunea formular care va avea dimensiuni fixate și va fi centrată */}
      <div className="form-section">
        {isSubmitted && messageVisible && (
          <div className="thank-you-message">
            <h3>Mulțumim pentru completare!</h3>
            <p>Formularul tău a fost trimis cu succes</p>
          </div>
        )}
          <form onSubmit={handleSubmit}>
            <div className="form-group">
            <label htmlFor="nume">Nume:</label>
            <input
              type="text"
              id="nume"
              name="nume"
              value={formData.nume}
              onChange={handleChange}
              required
              className="form-control"
            />
            {errors.nume && <span className="error-text">{errors.nume}</span>}
          </div>
          <div className="form-group">
            <label htmlFor="prenume">Prenume:</label>
            <input
              type="text"
              id="prenume"
              name="prenume"
              value={formData.prenume}
              onChange={handleChange}
              required
              className="form-control"
            />
            {errors.prenume && <span className="error-text">{errors.prenume}</span>}
          </div>
          <div className="form-group">
            <label htmlFor="facultate">Facultate:</label>
            <input
              type="text"
              id="facultate"
              name="facultate"
              value={formData.facultate}
              onChange={handleChange}
              required
              className="form-control"
            />
            {errors.facultate && <span className="error-text">{errors.facultate}</span>}
          </div>
          <div className="form-group">
            <label htmlFor="motivatie">Motivație (minim 100 cuvinte):</label>
            <textarea
              id="motivatie"
              name="motivatie"
              value={formData.motivatie}
              onChange={handleChange}
              required
              className="form-control"
            ></textarea>
            {errors.motivatie && <span className="error-text">{errors.motivatie}</span>}
          </div>
          


            <button type="submit" className="form-button">Trimite formularul</button>
          </form>
      </div>
    </div>
  );
};

export default FormPage;
