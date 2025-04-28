import React, { useState, useEffect } from 'react';
import { Search, X, Check } from 'lucide-react';

const MovieSelectionDialog = ({ isOpen, onClose, onAddMovies }) => {
  const [films, setFilms] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedMovies, setSelectedMovies] = useState([]);

  useEffect(() => {
    if (isOpen) {
      fetchMoviesFromBackend();
    }
  }, [isOpen]);

  const fetchMoviesFromBackend = async () => {
    try {
      const res = await fetch("https://localhost:44363/api/movies", {
        headers: {
          Accept: "application/json",
        },
      });

      const data = await res.json();
      console.log("BACKEND RAW:", data);

      const formatted = data.map((m) => ({
        id: m.Movie_id || m.movie_id || 0,
        title: m.Title || m.title || "Ismeretlen cím",
        year: m.Relase_Year || m.relase_Year || 0,
        posterUrl: m.Img_Url || m.img_Url,
        rating: 0,
      }));

      setFilms(formatted);
    } catch (err) {
      console.error("Hiba a filmek betöltésekor:", err);
    } finally {
      setIsLoading(false);
    }
  };

  const toggleMovieSelection = (movie) => {
    if (selectedMovies.some((m) => m.id === movie.id)) {
      setSelectedMovies(selectedMovies.filter((m) => m.id !== movie.id));
    } else {
      setSelectedMovies([...selectedMovies, movie]);
    }
  };

  const handleFinish = () => {
    onAddMovies(selectedMovies);
    onClose();
  };

  const filteredMovies = films.filter((movie) =>
    !searchTerm || movie?.title?.toLowerCase().includes(searchTerm.toLowerCase())
  );

  if (!isOpen) return null;

  return (
    <div className="movie-selection-overlay">
      <div className="movie-selection-dialog">
        <div className="movie-selection-header">
          <h2>Add Movies to Your Watchlist</h2>
          <button className="close-dialog-btn" onClick={onClose} aria-label="Close">
            <X size={24} />
          </button>
        </div>

        <div className="movie-selection-search">
          <Search className="search-icon" size={16} />
          <input
            type="text"
            className="search-input"
            placeholder="Search for movies..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>

        <div className="movie-selection-count">
          {selectedMovies.length} movies selected
        </div>

        {isLoading ? (
          <p>Loading movies...</p>
        ) : (
          <div className="movie-selection-grid">
            {filteredMovies.map((movie) => (
              <div
                key={movie.id}
                className={`movie-selection-card ${selectedMovies.some((m) => m.id === movie.id) ? 'selected' : ''}`}
                onClick={() => toggleMovieSelection(movie)}
              >
                <div className="selection-indicator">
                  {selectedMovies.some((m) => m.id === movie.id) && <Check size={20} />}
                </div>
                <img
                  src={
                    movie.posterUrl && movie.posterUrl.startsWith("http")
                      ? movie.posterUrl
                      : "/no-image.png"
                  }
                  alt={`${movie.title} poster`}
                  className="movie-poster"
                  onError={(e) => {
                    e.target.onerror = null;
                    e.target.src = "/no-image.png";
                  }}
                />
                <div className="movie-details">
                  <h3 className="movie-title">{movie.title}</h3>
                  <p className="movie-year">{movie.year}</p>
                </div>
              </div>
            ))}
          </div>
        )}

        <div className="movie-selection-actions">
          <button className="cancel-btn" onClick={onClose}>Cancel</button>
          <button className="finish-btn" onClick={handleFinish} disabled={selectedMovies.length === 0}>
            Add {selectedMovies.length} {selectedMovies.length === 1 ? 'Movie' : 'Movies'}
          </button>
        </div>
      </div>
    </div>
  );
};

export default MovieSelectionDialog;