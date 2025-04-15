// // // src/context/AuthContext.jsx
// // import React, { createContext, useState, useContext } from 'react';

// // const AuthContext = createContext();

// // export const useAuth = () => {
// //   return useContext(AuthContext);
// // };

// // export const AuthProvider = ({ children }) => {
// //   const [user, setUser] = useState(null);

// //   const login = (userData) => {
// //     setUser(userData); // Aici poți salva datele utilizatorului după autentificare
// //   };

// //   const logout = () => {
// //     setUser(null); // Aici poți reseta datele utilizatorului
// //   };

// //   return (
// //     <AuthContext.Provider value={{ user, login, logout }}>
// //       {children}
// //     </AuthContext.Provider>
// //   );
// // };

// // src/context/AuthContext.jsx
// // src/context/AuthContext.jsx
// src/context/AuthContext.jsx
import React, { createContext, useState, useContext, useEffect } from 'react';

const AuthContext = createContext();

// useAuth este un hook custom care folosește contextul
export const useAuth = () => {
  return useContext(AuthContext);  // Accesează contextul
};

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const login = (userData) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData)); // Salvează utilizatorul în localStorage
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem('user');
    localStorage.removeItem('token');
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
