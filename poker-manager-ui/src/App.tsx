import React, { useState, useEffect } from 'react';
import './App.css';
import Login from './components/Login';

function App() {
  const [token, setToken] = useState<string | null>(null);

  const handleLogin = (newToken: string) => {
    setToken(newToken);
    localStorage.setItem('token', newToken);
  };

  const handleLogout = () => {
    setToken(null);
    localStorage.removeItem('token');
  };

  useEffect(() => {
    const storedToken = localStorage.getItem('token');
    if (storedToken) {
      setToken(storedToken);
    }
  }, []);

  if (!token) {
    return <Login onLogin={handleLogin} />;
  }

  return (
    <div className="App">
      <nav className="navbar">
        <div className="navbar-brand">Poker Manager</div>
        <ul className="navbar-menu">
          <li><a href="#">Home</a></li>
          <li><a href="#">Games</a></li>
          <li><a href="#">Statistics</a></li>
          <li><a href="#">Profile</a></li>
          <li><button onClick={handleLogout}>Logout</button></li>
        </ul>
      </nav>
      <main>
        <h1>Welcome to Poker Manager</h1>
        <p>You are logged in!</p>
      </main>
    </div>
  );
}

export default App;