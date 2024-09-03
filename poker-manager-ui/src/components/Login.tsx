import React, { useState } from 'react';
import './Login.css';

function Login({ onLogin }: { onLogin: (token: string) => void }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setErrorMessage(''); // Clear any previous error messages
        const requestBody = { email: username, password };
        try {
            const response = await fetch('http://localhost:5145/api/auth/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(requestBody),
            });
            if (response.ok) {
                const data = await response.json();
                onLogin(data.token);
            } else {
                // Handle login error
                setErrorMessage('Login failed. Please check your credentials and try again.');
            }
        } catch (error) {
            setErrorMessage('An error occurred while trying to log in. Please try again later.');
        }
    };

    return (
        <div className="login-container">
            <div className="login-form">
                {errorMessage && <div className="error-message">{errorMessage}</div>}
                <h2>Sign in</h2>
                <form onSubmit={handleSubmit}>
                    <input
                        type="text"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        placeholder="Username"
                        required
                    />
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        placeholder="Password"
                        required
                    />
                    <button type="submit">Sign in</button>
                </form>
                <div className="social-login">
                    <p>or sign in with</p>
                    <div className="social-icons">
                        {/* Add social icons here */}
                    </div>
                </div>
            </div>
            <div className="welcome-message">
                <h2>Welcome back!</h2>
                <p>Welcome back! We are so happy to have you here. It's great to see you again. We hope you had a safe and enjoyable time away.</p>
                <p>No account yet? <a href="#">Sign up</a>.</p>
            </div>
        </div>
    );
}

export default Login;