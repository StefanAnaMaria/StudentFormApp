// import React from 'react';
// import { Navigate, Outlet } from 'react-router-dom';

// const PrivateRoute = ({ allowedRoles }) => {
//   const token = localStorage.getItem('token');
//   const role = localStorage.getItem('role')?.toLowerCase();
//   console.log("role", role);

//   if (!token) return <Navigate to="/login" replace />;

//   if (allowedRoles.includes(role)) {
//     return <Outlet />;
//   }

//   return <Navigate to={role === 'admin' ? "/formslist" : "/form"} replace />;
// };

// export default PrivateRoute;

import React from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';  // Importă hook-ul de autentificare

const PrivateRoute = ({ allowedRoles }) => {
  const { user } = useAuth();

  if (!user) {
    // Dacă utilizatorul nu este autentificat, redirecționează la pagina de login
    return <Navigate to="/login" replace />;
  }

  
      // Dacă user sau user.roles nu sunt disponibile, nu returnăm nimic
      if (!user || !user.roles) return null;
    
      // Asigură-te că user.roles este un array înainte de a utiliza map
      const hasAccess = allowedRoles.includes((user.roles) ? user.roles.map(r => r.name.toLowerCase()) : []);
    
  // Verifică dacă utilizatorul are unul dintre rolurile permise
  console.log("allowedRoles", allowedRoles);

  if (!hasAccess) {
    // Dacă utilizatorul nu are acces, redirecționează la o pagină de "acces interzis" sau altă pagină
    return <Navigate to="/no-acces" replace />;
  }

  // Dacă utilizatorul are acces, renderizează componenta
  return <Outlet />; 
};

export default PrivateRoute;