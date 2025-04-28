import React, { useEffect, useState, useRef } from "react";
import "../styles/Home.css";
import { Clock, ChevronLeft, ChevronRight, X } from "lucide-react";
import { useParams } from "react-router-dom";

export default function Profile() {
  const { id } = useParams();
  const isOtherUser = !!id;
  const [userInfo, setUserInfo] = useState({
    name: "",
    preferences: [],
    statistics: {},
  });
  const [favoriteMovies, setFavoriteMovies] = useState([]);
  const [plannedMovies, setPlannedMovies] = useState([]);
  const [watchedMovies, setWatchedMovies] = useState([]);
  const [loading, setLoading] = useState(true);

  const favoritesRef = useRef(null);
  const planRef = useRef(null);
  const watchedRef = useRef(null);

  const scroll = (direction, ref) => {
    const container = ref.current;
    const cardWidth = 200;
    const scrollAmount = direction === "left" ? -cardWidth * 2 : cardWidth * 2;
    container.scrollBy({ left: scrollAmount, behavior: "smooth" });
  };

  const loadProfile = () => {
    const userId = isOtherUser ? id : localStorage.getItem("userId");
    if (!userId) {
      setLoading(false);
      return;
    }

    fetch(`https://localhost:44363/api/users/profile/${parseInt(userId)}`)
      .then((res) => res.json())
      .then((data) => {
        setUserInfo({
          name: data.name,
          preferences: data.preferences,
          statistics: data.statistics,
        });
        setFavoriteMovies(data.favoriteMovies || []);
        setPlannedMovies(data.plannedMovies || []);
        setWatchedMovies(data.watched || []);
        setLoading(false);
      })
      .catch((err) => {
        console.error("Profile load error:", err);
        setLoading(false);
      });
  };

  useEffect(() => {
    loadProfile();
  }, [id]);

  const removeStatus = async (movieId, status) => {
    const userId = localStorage.getItem("userId");
    if (isOtherUser) return;

    try {
      await fetch(
        `https://localhost:44363/api/users/status/${userId}/${movieId}/${status}`,
        {
          method: "DELETE",
        }
      );

      if (status === "favorite") {
        setFavoriteMovies(
          favoriteMovies.filter((movie) => movie.movie_id !== movieId)
        );
        setUserInfo((prev) => ({
          ...prev,
          statistics: {
            ...prev.statistics,
            favorites: prev.statistics.favorites - 1,
          },
        }));
      } else if (status === "plan") {
        setPlannedMovies(
          plannedMovies.filter((movie) => movie.movie_id !== movieId)
        );
      } else if (status === "watched") {
        setWatchedMovies(
          watchedMovies.filter((movie) => movie.movie_id !== movieId)
        );
        setUserInfo((prev) => ({
          ...prev,
          statistics: {
            ...prev.statistics,
            totalWatched: prev.statistics.totalWatched - 1,
          },
        }));
      }

      localStorage.setItem("refreshHome", "true");
      localStorage.setItem("changedStatus", status);
    } catch (error) {
      console.error("Error removing status:", error);
    }
  };

  const renderMovieRow = (title, movieList, ref, statusKey) => (
    <div className="slider-section">
      {movieList && movieList.length > 0 ? (
        <>
          <div className="section-header">
            <h2>{title}</h2>
            <div className="slider-controls">
              <button onClick={() => scroll("left", ref)}>
                <ChevronLeft className="slider-icon" />
              </button>
              <button onClick={() => scroll("right", ref)}>
                <ChevronRight className="slider-icon" />
              </button>
            </div>
          </div>
          <div className="slider-container">
            <div className="movie-row" ref={ref}>
              {movieList.map((movie, index) => (
                <div
                  key={movie.movie_id || `${movie.Title}-${index}`}
                  className="movie-card"
                >
                  <div className="movie-card-inner">
                    <img
                      src={movie.img_Url}
                      alt={movie.title}
                      className="movie-poster"
                    />
                    <div className="movie-overlay">
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
                            {movie.release_Year}
                          </span>
                        </div>
                      </div>
                    </div>
                    {!isOtherUser && (
                      <button
                        className="remove-fav-btn"
                        onClick={() => removeStatus(movie.movie_id, statusKey)}
                        title="Remove"
                      >
                        <X size={20} color="white" />
                      </button>
                    )}
                  </div>
                </div>
              ))}
            </div>
          </div>
        </>
      ) : (
        <p>
          <h2>No {title.toLowerCase()} added yet.</h2>
        </p>
      )}
    </div>
  );

  if (loading) return <p>Loading...</p>;
  if (!userInfo.name) return <p>Could not load the profile.</p>;

  return (
    <div className="profile-container">
      <div className="profile-header">
        <div className="profile-info">
          <h2>{userInfo.name}</h2>
          <p>
            <strong>Preferences:</strong>{" "}
            {userInfo.preferences?.length > 0
              ? userInfo.preferences.join(", ")
              : "No preferences yet"}
          </p>
        </div>
      </div>

      <div className="profile-stats">
        <h3>Film Statistics</h3>
        <div className="stats-grid">
          <div className="stat-card">
            Total Watched: <span>{userInfo.statistics?.totalWatched ?? 0}</span>
          </div>
          <div className="stat-card">
            Favorites: <span>{userInfo.statistics?.favorites ?? 0}</span>
          </div>
          <div className="stat-card">
            Reviews Written:{" "}
            <span>{userInfo.statistics?.reviewsWritten ?? 0}</span>
          </div>
        </div>
      </div>

      {renderMovieRow(
        "Favorite Movies",
        favoriteMovies,
        favoritesRef,
        "favorite"
      )}
      {renderMovieRow("Plan to Watch", plannedMovies, planRef, "plan")}
      {renderMovieRow("Watched Movies", watchedMovies, watchedRef, "watched")}
    </div>
  );
}
