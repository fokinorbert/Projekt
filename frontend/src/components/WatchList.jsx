import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Star, Search, Heart, X, Plus, List, Trash2, Film } from "lucide-react";
import "../styles/WatchList.css";
import "../styles/MovieSelectionDialog.css";
import MovieSelectionDialog from "./MovieSelectionDialog";
import axios from "axios";

const WatchList = () => {
  const navigate = useNavigate();
  const [watchlists, setWatchlists] = useState([]);
  const [selectedWatchlist, setSelectedWatchlist] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [newListName, setNewListName] = useState("");
  const [isCreatingList, setIsCreatingList] = useState(false);
  const [isMovieDialogOpen, setIsMovieDialogOpen] = useState(false);
  const [listToDelete, setListToDelete] = useState(null);
  const [isDeleteConfirmOpen, setIsDeleteConfirmOpen] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const fetchWatchlistsWithMovies = async () => {
    try {
      const userId = parseInt(localStorage.getItem("userId"));
      const res = await fetch("https://localhost:44363/api/userlists");
      const lists = await res.json();
      console.log("API response:", lists);

      const userLists = lists.filter((list) => list.user_id === userId);

      const listsWithFilms = await Promise.all(
        userLists.map(async (list) => {
          const films = (list.films || []).map((m) => ({
            id: m.movie_id || m.Movie_id,
            title: m.title || m.Title,
            year: m.release_year || m.Release_Year || m.Relase_Year,
            posterUrl: m.img_url || m.Img_Url,
            rating: 0,
          }));

          const filmsWithRatings = await Promise.all(
            films.map(async (film) => {
              try {
                const ratingsResponse = await axios.get(
                  `https://localhost:44363/api/ratings/average/${film.id}`
                );
                console.log(
                  `Average rating for movie ${film.id}:`,
                  ratingsResponse.data
                );
                const avg =
                  ratingsResponse.data.averageRating ||
                  ratingsResponse.data.AverageRating ||
                  0;
                return {
                  ...film,
                  rating: avg,
                };
              } catch (avgError) {
                console.error(
                  `Error fetching average rating for movie ${film.id}:`,
                  avgError.response?.data || avgError.message
                );
                return {
                  ...film,
                  rating: 0,
                };
              }
            })
          );

          return {
            id: list.list_id,
            name: list.list_name,
            films: filmsWithRatings,
          };
        })
      );

      setWatchlists(listsWithFilms);
      return listsWithFilms;
    } catch (err) {
      console.error("Hiba az adatok betöltésekor:", err);
      return [];
    }
  };

  useEffect(() => {
    fetchWatchlistsWithMovies().then(() => setIsLoading(false));
  }, []);

  const handleCreateList = async () => {
    if (newListName.trim()) {
      try {
        const userId = parseInt(localStorage.getItem("userId"));

        const res = await fetch("https://localhost:44363/api/userlists", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            userId: userId,
            ListName: newListName.trim(),
          }),
        });

        const resText = await res.text();
        console.log("Backend válasz:", resText);

        if (res.ok) {
          await fetchWatchlistsWithMovies();
          setNewListName("");
          setIsCreatingList(false);
          console.log(`New list "${newListName}" saved to database`);
        } else if (res.status === 409) {
          alert("This list already exists!");
        } else {
          throw new Error("Lista létrehozása sikertelen");
        }
      } catch (err) {
        console.error("Hiba új lista létrehozásakor:", err);
      }
    }
  };

  const handleDeleteList = async () => {
    if (listToDelete) {
      try {
        const res = await fetch(
          `https://localhost:44363/api/userlists/${listToDelete.id}`,
          {
            method: "DELETE",
          }
        );

        if (res.ok) {
          setWatchlists((prev) =>
            prev.filter((list) => list.id !== listToDelete.id)
          );
          if (selectedWatchlist && selectedWatchlist.id === listToDelete.id) {
            setSelectedWatchlist(null);
          }
          console.log("Watchlist deleted from database");
        } else {
          console.error("Sikertelen törlés a backendben");
        }
      } catch (err) {
        console.error("Hiba a törlés során:", err);
      } finally {
        setIsDeleteConfirmOpen(false);
        setListToDelete(null);
      }
    }
  };

  const removeFromWatchlist = async (movieId) => {
    if (selectedWatchlist) {
      try {
        const res = await fetch(
          `https://localhost:44363/api/userlistsmovies/${selectedWatchlist.id}?MovieId=${movieId}`,
          {
            method: "DELETE",
          }
        );

        if (!res.ok) {
          console.error("Nem sikerült törölni a filmet az adatbázisból");
        }
      } catch (err) {
        console.error("Hiba a film törlésekor:", err);
      }

      const updatedLists = watchlists.map((list) =>
        list.id === selectedWatchlist.id
          ? {
              ...list,
              films: list.films.filter((film) => film.id !== movieId),
            }
          : list
      );
      setWatchlists(updatedLists);

      const updatedSelected = updatedLists.find(
        (list) => list.id === selectedWatchlist.id
      );
      setSelectedWatchlist(updatedSelected);

      console.log("Film törölve a listából és az adatbázisból");
    }
  };

  const handleAddMovies = async (selectedMovies) => {
    if (!selectedWatchlist) return;

    const newMoviesToAdd = selectedMovies.filter(
      (selectedMovie) =>
        !selectedWatchlist.films.some((film) => film.id === selectedMovie.id)
    );

    for (const movie of newMoviesToAdd) {
      try {
        const res = await fetch("https://localhost:44363/api/userlistsmovies", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            list_id: selectedWatchlist.id,
            movie_id: movie.id,
          }),
        });

        if (!res.ok) {
          console.error(`Nem sikerült hozzáadni a filmet: ${movie.title}`);
        }
      } catch (err) {
        console.error("Hiba a mentés közben:", err);
      }
    }

    const updatedLists = await fetchWatchlistsWithMovies();
    const updated = updatedLists.find((l) => l.id === selectedWatchlist.id);
    if (updated) setSelectedWatchlist(updated);

    console.log(`${newMoviesToAdd.length} film(ek) mentve és lista frissítve`);
  };

  const navigateToFilmInfo = (filmId) => {
    navigate(`/film-info/${filmId}`);
  };

  const filteredFilms = Array.isArray(selectedWatchlist?.films)
    ? selectedWatchlist.films.filter((film) =>
        (film?.title || "").toLowerCase().includes(searchTerm.toLowerCase())
      )
    : [];

  return (
    <div className="watchlist-container">
      <div className="watchlist-inner">
        {selectedWatchlist ? (
          <>
            <div className="watchlist-header-container">
              <button
                className="back-to-lists-btn"
                onClick={() => setSelectedWatchlist(null)}
              >
                <List size={18} />
                Back to Lists
              </button>
              <h1 className="watchlist-header">{selectedWatchlist.name}</h1>
            </div>

            <div className="actions-bar">
              <button
                className="create-list-btn"
                onClick={() => setIsMovieDialogOpen(true)}
              >
                <Plus size={20} />
                <span>Add Movies</span>
              </button>

              <div className="search-container">
                <Search className="search-icon" size={16} />
                <input
                  type="text"
                  className="search-input"
                  placeholder="Search in this watchlist..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                />
              </div>
            </div>

            {filteredFilms.length > 0 ? (
              <div className="watchlist-grid">
                {filteredFilms.map((film, index) => (
                  <div key={film.id || index} className="film-card-container">
                    <button
                      className="remove-btn"
                      onClick={(e) => {
                        e.stopPropagation();
                        removeFromWatchlist(film.id);
                      }}
                      aria-label="Remove film"
                    >
                      <X />
                    </button>
                    <div
                      className="film-card"
                      onClick={() => navigateToFilmInfo(film.id)}
                    >
                      <img
                        src={
                          film.posterUrl && film.posterUrl.startsWith("http")
                            ? film.posterUrl
                            : "/no-image.png"
                        }
                        alt={`${film.title} poster`}
                        className="film-poster"
                        onError={(e) => {
                          e.target.onerror = null;
                          e.target.src = "/no-image.png";
                        }}
                      />

                      <div className="film-details">
                        <h3 className="film-title">{film.title}</h3>
                        <p className="film-year">{film.year}</p>
                        <div className="film-rating">
                          <Star size={14} fill="#ffcc00" />
                          <span>{film.rating.toFixed(1)}</span>
                        </div>
                      </div>
                    </div>
                  </div>
                ))}
              </div>
            ) : (
              <div className="empty-list">
                <Film size={48} />
                <p className="empty-message">This watchlist is empty</p>
                <p>Add films you'd like to watch to keep track of them</p>
                <button
                  className="add-films-btn"
                  onClick={() => setIsMovieDialogOpen(true)}
                >
                  Add Movies
                </button>
              </div>
            )}
          </>
        ) : (
          <div className="watchlists-overview">
            <div className="watchlists-header">
              <h1 className="watchlist-header">Your Watchlists</h1>
              <button
                className="create-list-btn"
                onClick={() => setIsCreatingList(!isCreatingList)}
              >
                <Plus size={20} />
                <span>Create New List</span>
              </button>
            </div>

            {isCreatingList && (
              <div className="create-list-form">
                <input
                  type="text"
                  placeholder="Enter list name..."
                  value={newListName}
                  onChange={(e) => setNewListName(e.target.value)}
                  className="create-list-input"
                />
                <button
                  onClick={handleCreateList}
                  className="confirm-create-btn"
                >
                  Create
                </button>
                <button
                  onClick={() => setIsCreatingList(false)}
                  className="cancel-create-btn"
                >
                  Cancel
                </button>
              </div>
            )}

            {isLoading ? (
              <p>Loading watchlists...</p>
            ) : watchlists.length > 0 ? (
              <div className="watchlists-rows">
                {watchlists.map((list, index) => (
                  <div key={list.id || index} className="watchlist-item">
                    <div
                      className="watchlist-card"
                      onClick={() => setSelectedWatchlist(list)}
                    >
                      <div className="watchlist-icon">
                        <List size={24} />
                      </div>
                      <div className="watchlist-info">
                        <h3 className="watchlist-name">{list.name}</h3>
                        <p className="films-count">{list.films.length} films</p>
                        <div className="watchlist-preview">
                          {list.films.slice(0, 4).map((film, filmIndex) => (
                            <div
                              key={film.id || filmIndex}
                              className="preview-poster"
                            >
                              <img
                                src={
                                  film.posterUrl &&
                                  film.posterUrl.startsWith("http")
                                    ? film.posterUrl
                                    : "/no-image.png"
                                }
                                alt=""
                                onError={(e) => {
                                  e.target.onerror = null;
                                  e.target.src = "/no-image.png";
                                }}
                              />
                            </div>
                          ))}
                          {list.films.length > 4 && (
                            <div className="more-films">
                              +{list.films.length - 4}
                            </div>
                          )}
                        </div>
                      </div>
                    </div>
                    <button
                      className="delete-list-btn"
                      onClick={(e) => {
                        e.stopPropagation();
                        setListToDelete(list);
                        setIsDeleteConfirmOpen(true);
                      }}
                      aria-label="Delete list"
                    >
                      <Trash2 size={18} />
                    </button>
                  </div>
                ))}
              </div>
            ) : (
              <div className="empty-list">
                <Heart size={48} />
                <p className="empty-message">
                  You haven't created any watchlists yet
                </p>
                <p>
                  Create a watchlist to keep track of films you'd like to watch
                </p>
                <button
                  className="add-films-btn"
                  onClick={() => setIsCreatingList(true)}
                >
                  Create Your First Watchlist
                </button>
              </div>
            )}

            {isDeleteConfirmOpen && listToDelete && (
              <div className="delete-confirm-overlay">
                <div className="delete-confirm-dialog">
                  <h3>Delete Watchlist?</h3>
                  <p>
                    Are you sure you want to delete "{listToDelete.name}"? This
                    action cannot be undone.
                  </p>
                  <div className="delete-confirm-actions">
                    <button
                      className="cancel-delete-btn"
                      onClick={() => {
                        setIsDeleteConfirmOpen(false);
                        setListToDelete(null);
                      }}
                    >
                      Cancel
                    </button>
                    <button
                      className="confirm-delete-btn"
                      onClick={handleDeleteList}
                    >
                      Delete
                    </button>
                  </div>
                </div>
              </div>
            )}
          </div>
        )}

        <MovieSelectionDialog
          isOpen={isMovieDialogOpen}
          onClose={() => setIsMovieDialogOpen(false)}
          onAddMovies={handleAddMovies}
        />
      </div>
    </div>
  );
};

export default WatchList;
