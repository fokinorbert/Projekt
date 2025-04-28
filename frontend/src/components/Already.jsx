import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Already.css";

export default function Already() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleRegister = async () => {
    try {
      const response = await fetch("https://localhost:44363/api/users/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ Email: email, Password: password }),
      });

      if (!response.ok) {
        const errorText = await response.text();
        console.error("Login error:", response.status, errorText);
        setError("Invalid login. Please try again.");
        return;
      }

      const text = await response.text();
      const data = text ? JSON.parse(text) : null;
      console.log("Login success:", data);

      localStorage.setItem("userId", data.userId);

      navigate("/home");
    } catch (err) {
      console.error("Request error:", err);
      setError("A szerver nem elérhető. Indítsd el a backendet.");
    }
  };

  return (
    <div className="login-page">
      <div className="login-container">
        <div className="register-step">
          <h2 className="login-title">
            <span>
              <span className="highlight">Complete</span> Login
            </span>
          </h2>

          <div className="input-group">
            <input
              type="email"
              placeholder="Enter your email..."
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              className="login-input"
              autoFocus
            />
            <input
              type="password"
              placeholder="Enter your password..."
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              className="login-input"
            />
          </div>

          {error && <div className="error-message">{error}</div>}

          <div className="register-button-container">
            <button
              className="register-button"
              onClick={handleRegister}
              disabled={!email || !password}
            >
              Login
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
