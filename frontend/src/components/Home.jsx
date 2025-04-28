import { useState, useEffect, useRef, useLayoutEffect } from "react";
import { useNavigate } from "react-router-dom";
import About from "../components/About";
import axios from "axios";
import { ChevronLeft, ChevronRight, Clock } from "lucide-react";
import "../styles/Home.css";

import heartIcon from "../assets/heart.svg";
import checkIcon from "../assets/check.svg";
import bookmarkIcon from "../assets/bookmark.svg";

export default function Home() {
  const [movies, setMovies] = useState({
    recommended: [],
    unfinished: [],
    watched: [],
  });

  const [allGenres, setAllGenres] = useState([]);
  const [favoriteMovies, setFavoriteMovies] = useState([]);
  const [plannedMovies, setPlannedMovies] = useState([]);
  const [watchedMovies, setWatchedMovies] = useState([]);
  const [selectedGenre, setSelectedGenre] = useState("");
  const [genreMovies, setGenreMovies] = useState([]);
  const [scrollPositions, setScrollPositions] = useState({});

  const recommendedRef = useRef(null);
  const genreRef = useRef(null);
  const watchedRef = useRef(null);

  const navigate = useNavigate();
  const navigateToFilmInfo = (filmId) => {
    navigate(`/film-info/${filmId}`);
  };
  const userId = localStorage.getItem("userId");

  const loadProfileData = () => {
    if (userId) {
      axios
        .get(`https://localhost:44363/api/users/profile/${userId}`)
        .then((response) => {
          setMovies({
            recommended: response.data.recommendedMovies,
            unfinished: response.data.watching,
            watched: response.data.watched,
          });
          setFavoriteMovies(response.data.favoriteMovies || []);
          setPlannedMovies(response.data.plannedMovies || []);
          setWatchedMovies(response.data.watched || []);
        })
        .catch((error) => {
          console.error("Error fetching movies:", error);
        });
    }
  };

  useEffect(() => {
    loadProfileData();
  }, []);

  useEffect(() => {
    axios
      .get("https://localhost:44363/api/movies/genres")
      .then((response) => {
        setAllGenres(response.data || []);
      })
      .catch((error) => {
        console.error("Hiba a műfajok betöltésekor:", error);
      });
  }, []);

  useEffect(() => {
    const handleStorageChange = () => {
      const needsRefresh = localStorage.getItem("refreshHome") === "true";
      if (needsRefresh) {
        localStorage.setItem("refreshHome", "false");

        axios
          .get(`https://localhost:44363/api/users/profile/${userId}`)
          .then((response) => {
            setFavoriteMovies(response.data.favoriteMovies || []);
            setPlannedMovies(response.data.plannedMovies || []);
            setWatchedMovies(response.data.watched || []);
          })
          .catch((error) => {
            console.error("Státuszok frissítése hiba:", error);
          });
      }
    };

    window.addEventListener("storage", handleStorageChange);
    const interval = setInterval(() => {
      handleStorageChange();
    }, 500);

    return () => {
      window.removeEventListener("storage", handleStorageChange);
      clearInterval(interval);
    };
  }, [userId]);

  useEffect(() => {
    if (selectedGenre) {
      const scrollContainer = genreRef.current;
      const currentScroll = scrollContainer?.scrollLeft || 0;
      setScrollPositions((prev) => ({
        ...prev,
        [selectedGenre]: currentScroll,
      }));

      axios
        .get(`https://localhost:44363/api/movies/genre/${selectedGenre}`)
        .then((response) => {
          setGenreMovies(response.data || []);
        })
        .catch((error) => {
          console.error("Hiba a műfaj filmek betöltésekor:", error);
        });
    }
  }, [selectedGenre]);

  useLayoutEffect(() => {
    Object.entries(scrollPositions).forEach(([genre, position]) => {
      if (selectedGenre === genre) {
        const scrollContainer = genreRef.current;
        if (scrollContainer) {
          scrollContainer.scrollLeft = position;
        }
      }
    });
  }, [genreMovies]);

  const hasStatus = (movieId, list) => {
    return list.some((m) => m.movie_id === movieId);
  };

  const toggleStatus = async (movieId, status, e) => {
    e.stopPropagation();
    if (!userId) return;

    const isActive =
      (status === "favorite" && hasStatus(movieId, favoriteMovies)) ||
      (status === "plan" && hasStatus(movieId, plannedMovies)) ||
      (status === "watched" && hasStatus(movieId, watchedMovies));

    const url = `https://localhost:44363/api/users/status`;
    const body = { userId: parseInt(userId), movieId, status };

    if (isActive) {
      await fetch(`${url}/${userId}/${movieId}/${status}`, {
        method: "DELETE",
      });
    } else {
      await fetch(url, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(body),
      });
    }

    axios
      .get(`https://localhost:44363/api/users/profile/${userId}`)
      .then((response) => {
        setFavoriteMovies(response.data.favoriteMovies || []);
        setPlannedMovies(response.data.plannedMovies || []);
        setWatchedMovies(response.data.watched || []);
      })
      .catch((error) => {
        console.error("Státuszok frissítése hiba:", error);
      });

    localStorage.setItem("refreshHome", "true");
    setTimeout(() => {
      window.dispatchEvent(new Event("storage"));
    }, 100);
  };

  const scroll = (direction, ref) => {
    const container = ref.current;
    const scrollAmount = direction === "left" ? -400 : 400;
    container.scrollBy({
      left: scrollAmount,
      behavior: "smooth",
    });
  };

  const MovieSlider = ({ title, movies, sliderRef }) => (
    <section className="movie-section">
      <div className="section-header">
        <h2>{title}</h2>
        <div className="slider-controls">
          <button
            className="slider-control"
            onClick={() => scroll("left", sliderRef)}
            aria-label="Previous movies"
          >
            <ChevronLeft className="slider-icon" />
          </button>
          <button
            className="slider-control"
            onClick={() => scroll("right", sliderRef)}
            aria-label="Next movies"
          >
            <ChevronRight className="slider-icon" />
          </button>
        </div>
      </div>

      <div className="slider-container">
        <div className="movie-row" ref={sliderRef}>
          {movies.map((movie, index) => (
            <div
              key={movie.movie_id}
              className="movie-card"
              id={`fav-${movie.movie_id}`}
            >
              <div className="movie-card-inner">
                <img
                  src={movie.img_Url}
                  alt={movie.title}
                  className="movie-poster"
                />
                <div
                  className="movie-overlay"
                  onClick={() => navigateToFilmInfo(movie.movie_id)}
                >
                  <div className="movie-info">
                    <h3 className="movie-title">{movie.title}</h3>
                    {movie.director && (
                      <p className="movie-director">{movie.director}</p>
                    )}
                    <div className="movie-meta">
                      <span className="movie-duration">
                        <Clock size={14} />
                        {typeof movie.duration === "number"
                          ? `${Math.floor(movie.duration / 60)}h ${
                              movie.duration % 60
                            }m`
                          : "N/A"}
                      </span>
                      <span className="movie-year">
                        {movie.release_Year || movie.Release_Year}
                      </span>
                    </div>
                  </div>
                </div>
                <div className="status-icons">
                  <img
                    src={bookmarkIcon}
                    alt="Plan to Watch"
                    className="status-icon"
                    style={{
                      filter: hasStatus(movie.movie_id, plannedMovies)
                        ? "brightness(0) saturate(100%) invert(13%) sepia(100%) saturate(7486%) hue-rotate(260deg) brightness(92%) contrast(104%)"
                        : "brightness(0) saturate(100%) invert(100%)",
                    }}
                    onClick={(e) => toggleStatus(movie.movie_id, "plan", e)}
                  />
                  <img
                    src={heartIcon}
                    alt="Favorite"
                    className="status-icon"
                    style={{
                      filter: hasStatus(movie.movie_id, favoriteMovies)
                        ? "brightness(0) saturate(100%) invert(27%) sepia(78%) saturate(2819%) hue-rotate(339deg) brightness(91%) contrast(99%)"
                        : "brightness(0) saturate(100%) invert(100%)",
                    }}
                    onClick={(e) => toggleStatus(movie.movie_id, "favorite", e)}
                  />
                  <img
                    src={checkIcon}
                    alt="Watched"
                    className="status-icon"
                    style={{
                      filter: hasStatus(movie.movie_id, watchedMovies)
                        ? "invert(50%) sepia(87%) saturate(300%) hue-rotate(85deg) brightness(90%) contrast(120%)"
                        : "invert(0.5)",
                    }}
                    onClick={(e) => toggleStatus(movie.movie_id, "watched", e)}
                  />
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </section>
  );

  return (
    <div className="home">
      <div className="header">
        <h1>Welcome Back!</h1>
      </div>
      <About />

      <MovieSlider
        title="Recommended Movies"
        movies={movies.recommended}
        sliderRef={recommendedRef}
      />

      <div className="genre-selector">
        <label htmlFor="genreSelect">Select Genre:</label>
        <select
          id="genreSelect"
          onChange={(e) => setSelectedGenre(e.target.value)}
          value={selectedGenre}
        >
          <option value="">-- Choose a genre --</option>
          {allGenres.map((genre, idx) => (
            <option key={idx} value={genre}>
              {genre}
            </option>
          ))}
        </select>
      </div>

      {selectedGenre && genreMovies.length > 0 && (
        <MovieSlider
          title={`Movies in "${selectedGenre}"`}
          movies={genreMovies}
          sliderRef={genreRef}
        />
      )}
    </div>
  );
}
