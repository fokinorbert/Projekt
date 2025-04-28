import React, { useState, useRef, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Menu, Search, ChevronDown, X, Film } from "lucide-react";
import axios from "axios";
import "../styles/Header.css";

const Header = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const [isNavOpen, setIsNavOpen] = useState(false);
  const [searchQuery, setSearchQuery] = useState("");
  const [selectedCategory, setSelectedCategory] = useState("All");
  const [searchResults, setSearchResults] = useState([]);
  const menuRef = useRef(null);
  const navRef = useRef(null);
  const searchRef = useRef(null);
  const navigate = useNavigate();

  const toggleMenu = () => setIsMenuOpen(!isMenuOpen);
  const toggleNav = () => {
    setIsNavOpen(!isNavOpen);
    if (!isNavOpen) setSearchResults([]);
  };

  const handleSearchSubmit = (e) => {
    e.preventDefault();
    if (searchQuery.trim() && searchResults.length > 0) {
      if (selectedCategory === "movies") {
        navigate(`/film/${searchResults[0].id}`);
      } else if (selectedCategory === "users") {
        navigate(`/user/${searchResults[0].id}`);
      }
      setSearchQuery("");
      setSearchResults([]);
    }
  };

  const handleSearchChange = (e) => {
    const value = e.target.value;
    setSearchQuery(value);

    if (value.trim().length >= 2) {
      const endpoint =
        selectedCategory === "movies"
          ? `https://localhost:44363/api/movies/search?query=${encodeURIComponent(
              value
            )}`
          : `https://localhost:44363/api/users/search?query=${encodeURIComponent(
              value
            )}`;

      axios
        .get(endpoint)
        .then((res) => {
          setSearchResults(res.data);
        })
        .catch((err) => console.error("Search error:", err));
    } else {
      setSearchResults([]);
    }
  };

  const handleResultKeyDown = (e, item) => {
    if (e.key === "Enter" || e.key === " ") {
      e.preventDefault();
      navigate(
        selectedCategory === "movies" ? `/film/${item.id}` : `/user/${item.id}`
      );
      setSearchQuery("");
      setSearchResults([]);
    }
  };

  const handleClickOutside = (event) => {
    if (menuRef.current && !menuRef.current.contains(event.target)) {
      setIsMenuOpen(false);
    }
    if (navRef.current && !navRef.current.contains(event.target)) {
      setIsNavOpen(false);
    }
    if (searchRef.current && !searchRef.current.contains(event.target)) {
      setSearchResults([]);
    }
  };

  useEffect(() => {
    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, []);

  const categories = ["users", "movies"];

  return (
    <header className="header">
      <div className="logo">
        <Link to="/home" className="logo-link" aria-label="ListifyCinema Home">
          <Film className="logo-icon" />
          <span className="logo-text">ListifyCinema</span>
        </Link>
      </div>

      <div className="search-and-nav">
        <div className="search-wrapper" ref={searchRef}>
          <form
            onSubmit={handleSearchSubmit}
            className="search-bar"
            autoComplete="off"
            role="search"
          >
            <select
              value={selectedCategory}
              onChange={(e) => setSelectedCategory(e.target.value)}
              className="category-dropdown"
              aria-label="Search category"
            >
              {categories.map((category) => (
                <option key={category} value={category}>
                  {category.charAt(0).toUpperCase() + category.slice(1)}
                </option>
              ))}
            </select>
            <input
              type="search"
              placeholder="Search films or users..."
              value={searchQuery}
              onChange={handleSearchChange}
              aria-label="Search query"
            />
          </form>
          {searchResults.length > 0 && (
            <ul className="search-dropdown" role="listbox">
              {searchResults.map((item) => (
                <li
                  key={item.id}
                  className="search-result-item"
                  onClick={() =>
                    navigate(
                      selectedCategory === "movies"
                        ? `/film/${item.id}`
                        : `/user/${item.id}`
                    )
                  }
                  onKeyDown={(e) => handleResultKeyDown(e, item)}
                  tabIndex="0"
                  role="option"
                  aria-selected="false"
                >
                  {selectedCategory === "movies" ? (
                    <>
                      <img
                        src={item.posterUrl || "/default-poster.jpg"}
                        alt={item.title}
                        className="result-thumb"
                      />
                      <span>
                        {item.title} ({item.year})
                      </span>
                    </>
                  ) : (
                    <span>{item.username}</span>
                  )}
                </li>
              ))}
            </ul>
          )}
        </div>

        <button
          onClick={toggleNav}
          className="nav-toggle"
          aria-label={isNavOpen ? "Close menu" : "Open menu"}
          aria-expanded={isNavOpen}
        >
          {isNavOpen ? <X size={24} /> : <Menu size={24} />}
        </button>
      </div>

      <div
        className={`nav-links ${isNavOpen ? "open" : ""}`}
        ref={navRef}
        role="navigation"
        aria-label="Main navigation"
      >
        <div className="activity">
          <Link to="/activity">Activity</Link>
        </div>
        <div className="watchlist">
          <Link to="/lists">Watchlists</Link>
        </div>
        <div className="profile">
          <Link to="/profile">Profile</Link>
        </div>
      </div>
    </header>
  );
};

export default Header;
