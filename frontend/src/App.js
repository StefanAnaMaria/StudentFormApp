import React from 'react';
import { BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import Navbar from './components/Navbar';
import Sidebar from './components/Sidebar';
import LoginForm from './pages/LoginForm';
import { AuthProvider } from './context/AuthContext';
import FormPage from './pages/FormPage';
import './App.css';
import Home from './pages/Home';
import CompletedForms from './pages/CompletedForms';
import FormsList from './pages/FormsList';

const App = () => {

  const token = localStorage.getItem('token');
  // folosește hook-ul useNavigate pentru a redirecționa programatic
  return (
    <AuthProvider>
      <Router>
        <div className="appContainer">
          <Navbar />
          <div className="mainContent">
            {/* <Sidebar /> */}
            {token && <Sidebar />}
            <div className="pageContent">
              <Routes>
                <Route path="/" element={<Home/>} />
                <Route path="/login" element={<LoginForm />} />
                <Route path="/form" element={<FormPage />} />
                <Route path="/completed" element={<CompletedForms />} />
                <Route path="/formslist" element={<FormsList />}/>
                {/* <Route path="*" element={<LoginForm />} />  */}
              </Routes>
            </div>
          </div>
        </div>
      </Router>
    </AuthProvider>
  );
};


export default App;
