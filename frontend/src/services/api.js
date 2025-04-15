// src/services/api.js
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5042', // Înlocuiește cu URL-ul backend-ului tău
  timeout: 5000,
});

export default api;
