import { useEffect, useState } from 'react';
import './Pagesstyle.css';
import axios from 'axios';
import { useAuth } from '../context/AuthContext'; // Importă hook-ul de autentificare

export default function FormsList() {
  const [forms, setForms] = useState([]);
  const [editingFormId, setEditingFormId] = useState(null);
  const [editedForm, setEditedForm] = useState({});
  const { user } = useAuth();

  const userEmail = user ? user.email : 'email@exemplu.com';

  useEffect(() => {
    fetchForms();
  }, []);

  const fetchForms = async () => {
    try {
      const response = await fetch('http://localhost:5042/api/StudentForm/GetAllStudentForms',{
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        }
      });
      if (!response.ok) {
        throw new Error('Eroare la răspunsul serverului');
      }
      const data = await response.json();
      setForms(data);
    } catch (error) {
      console.error('Eroare la obținerea formularelor:', error);
    }
  };

  const handleDelete = async (formId) => {
    try {
      await axios.delete(`http://localhost:5042/api/StudentForm/DeleteStudentForm/${formId}`, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`,
        },
      });
      fetchForms();
    } catch (error) {
      console.error('Eroare la ștergerea formularului:', error);
    }
  };
  
 

  const handleEditClick = (form) => {
    setEditingFormId(form.idStudentForm);
    setEditedForm({ ...form });
  };

  const handleInputChange = (field, value) => {
    setEditedForm(prev => ({ ...prev, [field]: value ,userEmail  }));
  };

  const handleSave = async () => {
    // console.log('Trimitem la API:', editedForm); 
    try {
      await axios.put(`http://localhost:5042/api/StudentForm/UpdateStudentForm/${editedForm.idStudentForm}`, editedForm, {
        headers: {
          'Authorization': `Bearer ${localStorage.getItem('token')}`,
          'Content-Type': 'application/json',
        },
      });
      setEditingFormId(null);
      fetchForms();
    } catch (error) {
      console.error('Eroare la actualizarea formularului:', error);
    }
  };

  useEffect(() => {
    fetchForms();
  }, []);
  return (
    <div className="forms-list">
      <h2>Formulare completate</h2>
      <div className="forms-container">
        {forms.map(form => (
          <div key={form.idStudentForm} className="form-card">
            {editingFormId === form.idStudentForm ? (
              <>
                <div className="form-row">
                  <input
                    type="text"
                    value={editedForm.nume}
                    onChange={(e) => handleInputChange('nume', e.target.value)}
                  />
                  <input
                    type="text"
                    value={editedForm.prenume}
                    onChange={(e) => handleInputChange('prenume', e.target.value)}
                  />
                  <input
                    type="text"
                    value={editedForm.facultate}
                    onChange={(e) => handleInputChange('facultate', e.target.value)}
                  />
                </div>
                <div className="form-motivation">
                  <textarea
                    value={editedForm.motivatie}
                    onChange={(e) => handleInputChange('motivatie', e.target.value)}
                    rows={4}
                  />
                </div>
                <div className="form-actions">
                  <button className="edit-btn" onClick={handleSave}>Salvează</button>
                  <button className="delete-btn" onClick={() => setEditingFormId(null)}>Anulează</button>
                </div>
              </>
            ) : (
              <>
                <div className="form-row">
                  <span><strong>Nume:</strong> {form.nume}</span>
                  <span><strong>Prenume:</strong> {form.prenume}</span>
                  <span><strong>Facultate:</strong> {form.facultate}</span>
                </div>
                <div className="form-motivation">
                  <strong>Motivație:</strong>
                  <p>{form.motivatie}</p>
                </div>
                <div className="form-actions">
                  <button className="edit-btn" onClick={() => handleEditClick(form)}>Editează</button>
                  <button className="delete-btn" onClick={() => handleDelete(form.idStudentForm)}>Șterge</button>
                </div>
              </>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};
