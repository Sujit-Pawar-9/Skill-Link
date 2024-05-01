import React, { useState } from 'react';
import axios from 'axios';
import './ForgotPassword.css'; // Import your CSS file
import { useNavigate } from 'react-router-dom';
// ... (import statements)

const ForgotPassword = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [message, setMessage] = useState('');
  const navigate=useNavigate()
  const handleEmailChange = (e) => {
    setEmail(e.target.value);
  };

  const handlePasswordChange = (e) => {
    setPassword(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      // Implement additional validation before sending the request
      if (!email || !password) {
        setMessage('Please provide both email and password.');
        return;
      }

      const response = await axios.post('http://localhost:5020/api/User/forgotPassword',`Username=${email}&Password=${password}`);
      alert("password updated")
      navigate("/login")
      setMessage(response.data.message);
    } catch (error) {
      setMessage('Error: Unable to process your request. Please try again later.');
    }
  };

  return (
    <center>
      <div className="forgot-password-container">
        <div className="forgot-password-card">
          <h2>Forgot Password</h2>
          <form onSubmit={handleSubmit}>
            <label>Username:</label>
            <input
              type="text"
              value={email}
              onChange={handleEmailChange}
              required
            />
            <label>New Password:</label>
            <input
              type="password" // Use type="password" for password fields
              value={password}
              pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&^])[A-Za-z\d@.#$!%*?&]{8,15}$"
              onChange={handlePasswordChange}
              required
            />
            <button type="submit">Submit</button>
          </form>
          <p>{message}</p>
        </div>
      </div>
    </center>
  );
};

export default ForgotPassword;

