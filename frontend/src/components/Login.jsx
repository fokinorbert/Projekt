import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Login.css";

export default function Login() {
  const navigate = useNavigate();
  const [step, setStep] = useState(1);
  const [username, setUsername] = useState("");
  const [selectedPic, setSelectedPic] = useState(null);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [animateOut, setAnimateOut] = useState(false);

  const profilePictures = [
    "/img/agc.jpg",
    "/img/blue.jpeg",
    "/img/Solid_red.svg.png",
    "/img/green.jpg",
    "/img/Solid_yellow.svg.png",
  ];

  const handleNextStep = () => {
    if (step === 1 && username && selectedPic) {
      setAnimateOut(true);
      setTimeout(() => {
        setStep(2);
        setAnimateOut(false);
        navigate("/home");
      }, 300);
    }
  };

  return (
    <div className="login-page">
      <div className="login-container">
        {step === 1 && (
          <div
            className={`username-step ${
              animateOut ? "animate-out" : "animate-in"
            }`}
          >
            <h2 className="login-title">
              <span>
                <span className="highlight">Choose</span> a Username
              </span>
            </h2>

            <div className="input-group">
              <input
                type="text"
                placeholder="Enter your username..."
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                className="login-input"
                autoFocus
              />
            </div>

            <div className="profile-section">
              <h3 className="login-subtitle">Select a Profile Picture</h3>
              <div className="profile-pic-grid">
                {profilePictures.map((pic, index) => (
                  <img
                    key={index}
                    src={pic}
                    alt="Profile"
                    className={`profile-pic ${
                      selectedPic === pic ? "selected" : ""
                    }`}
                    onClick={() => setSelectedPic(pic)}
                  />
                ))}
              </div>
            </div>

            <div className="button-container">
              <button
                className="next-button"
                onClick={handleNextStep}
                disabled={!username || !selectedPic}
                aria-label="Continue to next step"
              >
                <svg
                  fill="#fff"
                  height="800px"
                  width="800px"
                  version="1.1"
                  id="Layer_1"
                  xmlns="http://www.w3.org/2000/svg"
                  xmlns:xlink="http://www.w3.org/1999/xlink"
                  viewBox="0 0 330 330"
                  xml:space="preserve"
                >
                  <path
                    id="XMLID_27_"
                    d="M15,180h263.787l-49.394,49.394c-5.858,5.857-5.858,15.355,0,21.213C232.322,253.535,236.161,255,240,255
                    s7.678-1.465,10.606-4.394l75-75c5.858-5.857,5.858-15.355,0-21.213l-75-75c-5.857-5.857-15.355-5.857-21.213,0
                    c-5.858,5.857-5.858,15.355,0,21.213L278.787,150H15c-8.284,0-15,6.716-15,15S6.716,180,15,180z"
                  />
                </svg>
              </button>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
