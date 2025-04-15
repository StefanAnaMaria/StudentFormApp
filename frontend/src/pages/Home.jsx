import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div style={styles.container}>
      <h1 style={styles.title}>Bine ai venit în StudentForm 📄</h1>
      <p style={styles.subtitle}>
        Aplicație de gestionare formulare și date pentru studenți și administratori.
      </p>
      <Link to="/login" style={styles.button}>Intră în cont</Link>
    </div>
  );
};

const styles = {
  container: {
    padding: '100px 20px',
    textAlign: 'center',
    backgroundColor: '#f5f5f5',
    height: '100vh'
  },
  title: {
    fontSize: '3rem',
    marginBottom: '20px'
  },
  subtitle: {
    fontSize: '1.5rem',
    marginBottom: '40px'
  },
  button: {
    padding: '15px 30px',
    backgroundColor: '#333',
    color: 'white',
    textDecoration: 'none',
    borderRadius: '8px',
    fontSize: '1.1rem'
  }
};

export default Home;
