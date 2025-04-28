export default function Reviews({ reviews }) {
  return (
    <div className="reviews-section">
      <h3>Recent Ratings</h3>
      {reviews.length === 0 ? (
        <p>No ratings yet.</p>
      ) : (
        <ul>
          {reviews.map((r, index) => (
            <li key={index}>
              <strong>{r.movie.title}</strong> – ⭐ {r.rating}/5
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
