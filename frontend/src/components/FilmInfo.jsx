import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ArrowLeft, Star, Eye, MessageSquare } from "lucide-react";
import "../styles/FilmInfo.css";
import axios from "axios";

const FilmInfo = () => {
  const { filmId } = useParams();
  const navigate = useNavigate();
  const [film, setFilm] = useState(null);
  const [error, setError] = useState(null);
  const [commentVisible, setCommentVisible] = useState(false);
  const [commentText, setCommentText] = useState("");
  const [selectedRating, setSelectedRating] = useState(0);
  const [isModifying, setIsModifying] = useState(false);
  const [averageRating, setAverageRating] = useState(0);
  const [totalRaters, setTotalRaters] = useState(0);
  const [comments, setComments] = useState([]);

  const userId = localStorage.getItem("userId");

  useEffect(() => {
    let isMounted = true;

    const fetchData = async () => {
      try {
        if (!userId || !filmId) {
          throw new Error("Missing userId or filmId");
        }

        const filmResponse = await axios.get(
          `https://localhost:44363/api/movies/${filmId}`
        );
        if (isMounted) {
          console.log("Film data:", filmResponse.data);
          setFilm(filmResponse.data);
          setError(null);
        }

        try {
          const userRatingResponse = await axios.get(
            `https://localhost:44363/api/ratings/${userId}`
          );
          if (isMounted) {
            console.log("Raw user ratings response:", userRatingResponse.data);
            const parsedFilmId = parseInt(filmId);
            const userRating = userRatingResponse.data.find(
              (rating) => parseInt(rating.movie_id) === parsedFilmId
            );
            if (userRating) {
              console.log("User's rating for this film:", userRating.rating);
              setSelectedRating(userRating.rating);
              setIsModifying(false);
            } else {
              console.log("No rating found for this film by the user.");
              setSelectedRating(0);
              setIsModifying(true);
            }
          }
        } catch (userRatingError) {
          console.error(
            "User rating fetch error:",
            userRatingError.response?.data || userRatingError.message
          );
          if (isMounted) {
            setSelectedRating(0);
            setIsModifying(true);
          }
        }

        try {
          const ratingsResponse = await axios.get(
            `https://localhost:44363/api/ratings/average/${filmId}`
          );
          if (isMounted) {
            console.log("Average rating response:", ratingsResponse.data);
            if (ratingsResponse.data) {
              const avg =
                ratingsResponse.data.averageRating ||
                ratingsResponse.data.AverageRating ||
                0;
              const total =
                ratingsResponse.data.totalRaters ||
                ratingsResponse.data.TotalRaters ||
                0;
              setAverageRating(avg.toFixed(1));
              setTotalRaters(total);
              console.log(
                "Set averageRating:",
                avg.toFixed(1),
                "totalRaters:",
                total
              );
            } else {
              console.warn("No average rating data received.");
              setAverageRating(0);
              setTotalRaters(0);
            }
          }
        } catch (avgError) {
          console.error(
            "Average rating fetch error:",
            avgError.response?.data || avgError.message
          );
          if (isMounted) {
            setAverageRating(0);
            setTotalRaters(0);
          }
        }

        try {
          const commentsResponse = await axios.get(
            `https://localhost:44363/api/comments/movie/${filmId}`
          );
          if (isMounted) {
            console.log("Comments:", commentsResponse.data);
            setComments(commentsResponse.data);
          }
        } catch (commentsError) {
          console.error(
            "Comments fetch error:",
            commentsError.response?.data || commentsError.message
          );
        }
      } catch (err) {
        if (isMounted) {
          console.error("Fetch error:", err.response?.data || err.message);
          setError("Failed to fetch data. Please try again later.");
        }
      }
    };

    fetchData();

    return () => {
      isMounted = false;
    };
  }, [filmId, userId]);

  const handleCommentSubmit = async () => {
    if (!film) return alert("Film adatok nem elérhetők.");

    const payload = {
      User_id: parseInt(userId),
      Movie_id: film.movie_id,
      Comment: commentText,
      Created_At: new Date().toISOString(),
    };

    try {
      await axios.post("https://localhost:44363/api/comments", payload);
      alert("Komment elküldve!");
      setCommentText("");
      setCommentVisible(false);

      const commentsResponse = await axios.get(
        `https://localhost:44363/api/comments/movie/${filmId}`
      );
      setComments(commentsResponse.data);
    } catch (err) {
      console.error(
        "Comment submission error:",
        err.response?.data || err.message
      );
      alert("Hiba történt a komment küldésekor.");
    }
  };

  const handleRatingClick = async (value) => {
    if (!film) return alert("Film adatok nem elérhetők.");
    if (!isModifying) return;

    setSelectedRating(value);

    const payload = {
      User_id: parseInt(userId),
      Movie_id: film.movie_id,
      Rating: value,
    };

    try {
      await axios.post("https://localhost:44363/api/ratings", payload);
      alert("Értékelés elküldve!");
      setIsModifying(false);

      try {
        const ratingsResponse = await axios.get(
          `https://localhost:44363/api/ratings/average/${filmId}`
        );
        const avg =
          ratingsResponse.data.averageRating ||
          ratingsResponse.data.AverageRating ||
          0;
        const total =
          ratingsResponse.data.totalRaters ||
          ratingsResponse.data.TotalRaters ||
          0;
        setAverageRating(avg.toFixed(1));
        setTotalRaters(total);
      } catch (avgError) {
        console.error(
          "Average rating refresh error:",
          avgError.response?.data || avgError.message
        );
        setAverageRating(0);
        setTotalRaters(0);
      }
    } catch (err) {
      console.error(
        "Rating submission error:",
        err.response?.data || err.message
      );
      alert("Hiba történt az értékelés során.");
      setSelectedRating(0);
    }
  };

  const handleModifyClick = () => {
    setIsModifying(true);
    setSelectedRating(0);
  };

  if (error) {
    return (
      <div className="film-not-found">
        <h2>{error}</h2>
        <button onClick={() => navigate("/home")} className="back-button">
          Back to Home
        </button>
      </div>
    );
  }

  if (!film) {
    return <div>Loading...</div>;
  }

  const posterSrc =
    film.img_Url && film.img_Url.startsWith("http")
      ? film.img_Url
      : `/posters/${film.img_Url}`;

  return (
    <div className="film-info-container">
      <div className="film-info-content">
        <div className="film-info-header">
          <button className="back-button" onClick={() => navigate(-1)}>
            <ArrowLeft size={20} /> Back
          </button>
        </div>

        <div className="film-info-main">
          <div className="film-poster-container">
            <img
              src={posterSrc}
              alt={`${film.title} poster`}
              className="film-poster-large"
              onError={(e) => (e.currentTarget.src = "/default-poster.jpg")}
            />
          </div>

          <div className="film-details-container">
            <h1 className="film-title-large">{film.title}</h1>
            <div className="film-meta">
              <span>{film.release_Year}</span>
              <span>Directed by {film.director}</span>
              <span>
                {Math.floor(film.duration / 60)} h{" "}
                {Math.floor(film.duration % 60)} m
              </span>
              {film.description && (
                <p>
                  <strong>Description:</strong> {film.description}
                </p>
              )}
              <p>
                <strong>Rating:</strong> {averageRating} ⭐ / {totalRaters}{" "}
                votes
              </p>
            </div>

            <div className="film-actions">
              <div className="action-group">
                <button
                  className="action-button"
                  onClick={() => setCommentVisible(!commentVisible)}
                >
                  <MessageSquare size={18} />
                  <span>Comment</span>
                </button>
              </div>

              {commentVisible && (
                <div className="comment-section">
                  <textarea
                    value={commentText}
                    onChange={(e) => setCommentText(e.target.value)}
                    placeholder="Írd ide a véleményed..."
                    className="comment-textarea"
                  />
                  <button
                    className="submit-button"
                    onClick={handleCommentSubmit}
                  >
                    Küldés
                  </button>
                </div>
              )}

              <div className="rating-container">
                <p>Your Rating:</p>
                <div className="star-rating">
                  {[1, 2, 3, 4, 5].map((star) => (
                    <Star
                      key={star}
                      size={24}
                      fill={star <= selectedRating ? "#FFD700" : "transparent"}
                      stroke="#FFD700"
                      style={{ cursor: isModifying ? "pointer" : "default" }}
                      onClick={() => handleRatingClick(star)}
                    />
                  ))}
                </div>
                {selectedRating > 0 && !isModifying && (
                  <button
                    className="modify-button"
                    onClick={handleModifyClick}
                    style={{ marginLeft: "10px" }}
                  >
                    Modify
                  </button>
                )}
              </div>
            </div>
          </div>
        </div>

        <div className="comments-section">
          <h2 className="comments-title">Kommentek</h2>
          {comments.length === 0 ? (
            <p>Még nincsenek kommentek ehhez a filmhez.</p>
          ) : (
            <ul className="comments-list">
              {comments.map((comment) => (
                <li key={comment.comment_id} className="comment-item">
                  <p className="comment-text">
                    <strong>{comment.userName}</strong>:{" "}
                    {comment.comment || "Nincs szöveg"}
                  </p>
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>
    </div>
  );
};

export default FilmInfo;
