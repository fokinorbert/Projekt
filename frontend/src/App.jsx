import axios from "axios";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useState, useEffect } from "react";
import Loader from "./components/Loader";
import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";
import ProfilePage from "./pages/ProfilePage";
import ActivityPage from "./pages/ActivityPage";
import ListsPage from "./pages/ListsPage";
import FilmPage from "./pages/FilmPage";
import Register from "./components/Register";
import Already from "./components/Already";
import FilmInfo from "./components/FilmInfo";
import "./styles/Global.css";
import "./styles/Login.css";
import "./styles/Register.css";
import "./styles/Preferences.css";
import "./styles/Home.css";
import "./styles/Profile.css";
import "./styles/Footer.css";
import "./styles/Loader.css";
import "./styles/Comments.css";
import "./styles/Header.css";
import { Film } from "lucide-react";
import { Navigate } from "react-router-dom";
import Profile from "./components/Profile";
import Comments from "./components/Comments";
import Reviews from "./components/Reviews";

function App() {
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => setIsLoading(false), 2000);
    return () => clearTimeout(timer);
  }, []);

  if (isLoading) {
    return <Loader />;
  }

  return (
    <Router>
      <Routes>
        <Route index path="/register" element={<Register />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/home" element={<HomePage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/user/:id" element={<Profile />} />
        <Route path="/activity" element={<ActivityPage />} />
        <Route path="/lists" element={<ListsPage />} />
        <Route path="/film" element={<FilmPage />} />
        <Route path="/already" element={<Already />} />
        <Route path="/film-info/:filmId" element={<FilmInfo />} />
        <Route path="/" element={<Navigate to="/register" replace />} />
        <Route path="/film/:filmId" element={<FilmInfo />} />
      </Routes>
    </Router>
  );
}

export default App;
