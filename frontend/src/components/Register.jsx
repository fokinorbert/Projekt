import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "../styles/Register.css";

const Register = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");
  const [selectedPreferences, setSelectedPreferences] = useState([]);
  const [selectedActors, setSelectedActors] = useState([]);
  const [actorQuery, setActorQuery] = useState("");
  const [actorSuggestions, setActorSuggestions] = useState([]);
  const [allActors, setAllActors] = useState([]);

  const categories = ["Action", "Romance", "Thriller", "Horror", "Comedy"];

  useEffect(() => {
    fetch("https://localhost:44363/api/persons")
      .then((res) => res.json())
      .then((data) => {
        const isActorOrDirector = (role) => {
          const cleanRole = role.trim().toLowerCase();
          return cleanRole === "actor" || cleanRole === "director";
        };

        const filtered = data.filter((p) => isActorOrDirector(p.role));
        filtered.sort((a, b) => a.name.localeCompare(b.name));
        setAllActors(filtered.map((p) => p.name));
      })
      .catch((err) => {
        console.error("Hiba a szereplők lekérésekor:", err);
      });
  }, []);

  const togglePreference = (category) => {
    setSelectedPreferences((prev) =>
      prev.includes(category)
        ? prev.filter((cat) => cat !== category)
        : [...prev, category]
    );
  };

  const handleActorSearch = (e) => {
    const query = e.target.value;
    setActorQuery(query);

    if (query.length > 0) {
      const matches = allActors.filter((actor) =>
        actor.toLowerCase().includes(query.toLowerCase())
      );
      setActorSuggestions(matches);
    } else {
      setActorSuggestions([]);
    }
  };

  const addActor = (actor) => {
    if (!selectedActors.includes(actor)) {
      setSelectedActors([...selectedActors, actor]);
    }
    setActorQuery("");
    setActorSuggestions([]);
  };

  const removeActor = (actorToRemove) => {
    setSelectedActors(
      selectedActors.filter((actor) => actor !== actorToRemove)
    );
  };

  const handleRegister = (e) => {
    e.preventDefault();

    if (!username || !email || !password || !confirmPassword) {
      setError("All fields must be filled out!");
      return;
    }

    if (password !== confirmPassword) {
      setError("Passwords do not match!");
      return;
    }

    if (selectedPreferences.length === 0 || selectedActors.length === 0) {
      setError("Please select at least one from both Preferences and Actor!");
      return;
    }

    setError("");

    fetch("https://localhost:44363/api/users/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        UserName: username,
        Password: password,
        Email: email,
        Preferences: selectedPreferences,
        FavoriteActors: selectedActors,
      }),
    })
      .then(async (response) => {
        if (response.status === 409) {
          const errorText = await response.text();
          console.warn("Conflict:", errorText);
          setError("Ez az e-mail vagy felhasználónév már használatban van.");
          return;
        }

        if (!response.ok) {
          const errorText = await response.text();
          console.error("Server error:", response.status, errorText);
          setError("Registration failed. Try again later.");
          return;
        }

        const text = await response.text();
        const data = text ? JSON.parse(text) : null;

        console.log("Success:", data);
        navigate("/already");
      })
      .catch((err) => {
        console.error("Request error:", err);
        setError("Something went wrong. Please try again.");
      });
  };

  return (
    <div className="auth-page">
      <div className="auth-box">
        <h1 className="auth-title">Create Account</h1>
        <form onSubmit={handleRegister}>
          <div className="input-group">
            <input
              type="text"
              placeholder="Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <input
              type="password"
              placeholder="Confirm Password"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
            />
          </div>

          <div className="preferences-container">
            <span className="preferences-title">Preferences:</span>
            <div className="category-tags">
              {categories.map((category) => (
                <button
                  key={`pref-${category}`}
                  type="button"
                  className={`category-tag ${
                    selectedPreferences.includes(category) ? "selected" : ""
                  }`}
                  onClick={() => togglePreference(category)}
                >
                  {category}
                </button>
              ))}
            </div>

            <span className="preferences-title">Actor:</span>
            <div className="actor-search">
              <input
                type="text"
                placeholder="Search actor..."
                value={actorQuery}
                onChange={handleActorSearch}
              />
              {actorSuggestions.length > 0 && (
                <ul className="suggestions-list">
                  {actorSuggestions.map((actor, index) => (
                    <li key={index} onClick={() => addActor(actor)}>
                      {actor}
                    </li>
                  ))}
                </ul>
              )}
            </div>

            <div className="selected-actors">
              {selectedActors.map((actor, index) => (
                <div key={index} className="selected-actor">
                  {actor}
                  <span
                    className="remove-actor"
                    onClick={() => removeActor(actor)}
                  >
                    &times;
                  </span>
                </div>
              ))}
            </div>
          </div>

          {error && <div className="error-message">{error}</div>}

          <button type="submit" className="auth-button">
            Register
          </button>

          <div className="auth-links">
            <p>
              Already have an account?{" "}
              <span className="link" onClick={() => navigate("/already")}>
                Log In
              </span>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Register;
