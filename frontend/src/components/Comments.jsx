export default function Comments({ comments }) {
  return (
    <div className="comments-section">
      <h3>Recent Comments</h3>
      {comments.length === 0 ? (
        <p>No comments yet.</p>
      ) : (
        <ul>
          {comments.map((c, index) => (
            <li key={index}>
              <strong>{c.movie.title}</strong> – {c.text}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}
