import { useState, useEffect } from "react";
import axios from "axios";
import Comments from "../components/Comments";
import Reviews from "../components/Reviews";

export default function Activity() {
  const [activity, setActivity] = useState(null);

  useEffect(() => {
    const userId = localStorage.getItem("userId");

    const fetchActivity = async () => {
      try {
        const [commentsRes, ratingsRes] = await Promise.all([
          axios.get(`https://localhost:44363/api/comments/${userId}`),
          axios.get(`https://localhost:44363/api/ratings/${userId}`),
        ]);

        const comments = commentsRes.data.map((c) => ({
          movie: {
            title: c.movieTitle,
            imgUrl: c.imgurl,
          },
          text: c.comment,
          date: c.Created_At?.split("T")[0] || "",
        }));

        const reviews = ratingsRes.data.map((r) => ({
          movie: {
            title: r.movieTitle,
            imgUrl: r.imgurl,
          },
          rating: r.rating,
          date: "",
        }));

        setActivity({
          comments: comments.reverse(),
          reviews: reviews.reverse(),
          watchlist: [],
        });
      } catch (error) {
        console.error("Activity fetch error:", error);
      }
    };

    fetchActivity();
  }, []);

  if (!activity) return <p className="loading-text">Loading activity...</p>;

  return (
    <div className="activity-container">
      <h2 className="activity-title">Recent Activity</h2>
      <Reviews reviews={activity.reviews} />
      <Comments comments={activity.comments} />
    </div>
  );
}
