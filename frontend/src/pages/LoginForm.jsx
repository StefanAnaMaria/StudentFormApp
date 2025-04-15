import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext'; // Importăm AuthContext

const LoginForm= () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();
  const [error, setError] = useState('');
  const { login } = useAuth();

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const res = await axios.post('http://localhost:5042/api/autentificare/login', {
        email,
        password
      });

       // Destructurăm datele corect din response
    const { token, email: userEmail, roles } = res.data; 

    // Salvează corect în localStorage
    localStorage.setItem('token', token);
    localStorage.setItem('user', JSON.stringify({ email: userEmail, roles }));
    localStorage.setItem('userEmail',userEmail); // 🔥 ADĂUGĂ ASTA


      // Apelăm funcția login din AuthContext pentru a seta user-ul în context
      login({email, roles});
      // ✅ Afișezi în consolă
      console.log('Token primit:', token);
      console.log(' emai/role:', userEmail, roles);
      console.log('Autentificare cu succes!');

      navigate('/dashboard');
    } catch (err) {
      console.error('Eroare la login:', err);
      alert('Email sau parolă incorectă.');
    }
  };

  return (
    <div style={styles.container}>
     <form onSubmit={handleLogin} style={styles.form}>
        <h2 style={styles.title}>Autentificare</h2>
        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
          required
          style={styles.input}
        />
        <input
          type="password"
          placeholder="Parolă"
          value={password}
          onChange={e => setPassword(e.target.value)}
          required
          style={styles.input}
        />
        <button type="submit" style={styles.button}>Login</button>
      </form>
    </div>
  );
};
const styles = {
    container: {
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      height: '100vh',
      backgroundColor: '#f5f5f5',
    },
    form: {
      backgroundColor: '#fff',
      padding: '40px',
      borderRadius: '10px',
      boxShadow: '0 8px 24px rgba(0,0,0,0.1)',
      display: 'flex',
      flexDirection: 'column',
      width: '100%',
      maxWidth: '400px'
    },
    title: {
      textAlign: 'center',
      marginBottom: '30px',
      fontSize: '24px',
      color: '#333'
    },
    input: {
      padding: '12px',
      marginBottom: '20px',
      borderRadius: '6px',
      border: '1px solid #ccc',
      fontSize: '16px'
    },
    button: {
      padding: '12px',
      backgroundColor: '#333',
      color: '#fff',
      fontSize: '16px',
      border: 'none',
      borderRadius: '6px',
      cursor: 'pointer',
      transition: 'background 0.3s ease'
    }
  };
export default LoginForm;
