// 📁 src/components/Sidebar.jsx
import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Sidebar = () => {
  
    const { user } = useAuth();

    // Dacă user sau user.roles nu sunt disponibile, nu returnăm nimic
    if (!user || !user.roles) return null;
  
    // Asigură-te că user.roles este un array înainte de a utiliza map
    const roles = Array.isArray(user.roles) ? user.roles.map(r => r.name.toLowerCase()) : [];
    return (
      <div style={styles.sidebar}>
        <h3 style={styles.title}>Navigare</h3>
        <ul style={styles.list}>
          {roles.includes('admin') ? (
            <>
             <li><Link to="/formslist" style={styles.link}>Forms</Link></li>

            </>
          ) : (
            <>
              <li><Link to="/form" style={styles.link}>Form</Link></li>
              <li><Link to="/completed" style={styles.link}>Form Completed</Link></li>
            </>
          )}
        </ul>
      </div>
    );  
};


const styles = {
  sidebar: {
    width: '200px',
    backgroundColor: '#f4f4f4',
    padding: '20px',
    height: '100vh',
  },
  title: {
    fontWeight: 'bold',
    fontSize: '18px',
    marginBottom: '10px',
  },
  list: {
    listStyle: 'none',
    padding: 0,
  },
  link: {
    display: 'block',
    color: '#333',
    textDecoration: 'none',
    padding: '8px 0',
  },
};

export default Sidebar;
