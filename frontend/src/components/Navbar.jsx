import React from 'react';
import { Link, useNavigate } from 'react-router-dom';

const Navbar = () => {
  const navigate = useNavigate();
  const token = localStorage.getItem('token');

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    navigate('/login');
  };

  return (
    <nav style={styles.navbar}>
      <ul style={styles.navList}>
        {!token ? (
          <>
            <li><Link to="/" style={styles.navItem}>Home</Link></li>
            <li><Link to="/login" style={styles.buttonItem}>Login</Link></li>
          </>
        ) : (
          <>
            {/* <li><Link to="/dashboard" style={styles.navItem}>Dashboard</Link></li> */}
            <li><button onClick={handleLogout} style={styles.buttonItem}>Logout</button></li>
          </>
        )}
      </ul>
    </nav>
  );
};


const styles = {
  navbar: {
    backgroundColor: '#333',
    padding: '10px 0',
    display: 'flex',
    justifyContent: 'flex-end',
    alignItems: 'center',
  },
  navList: 
  { listStyle: 'none',
  display: 'flex',
  justifyContent: 'center',
  alignItems: 'center',
  marginRight: '20px',
},
  navItem: {
    color: 'white',
    background: 'none',
    border: 'none',
    textDecoration: 'none',
    fontSize: '16px',
    cursor: 'pointer',
    display: 'flex',
    alignItems: 'center',
    padding: '8px 16px',
  },
  buttonItem: {
    color: '#ddd', // gri deschis
    backgroundColor: '#444', // fundal mai închis
    border: '1px solid #555',
    borderRadius: '6px',
    padding: '8px 16px',
    textDecoration: 'none',
    fontSize: '16px',
    cursor: 'pointer',
    transition: 'all 0.3s ease',
  },  
};

export default Navbar;
