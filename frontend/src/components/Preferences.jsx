
export default function Preferences() {
  return (
    <div className="preferences">
      <h1>Set Your Preferences</h1>
      <form>
        <label>Select Favorite Actors:</label>
        <input type="text" placeholder="E.g., Leonardo DiCaprio" />
        <label>Select Genres:</label>
        <select multiple>
          <option>Action</option>
          <option>Romantic</option>
          <option>Comedy</option>
          <option>Horror</option>
        </select>
        <button type="submit">Save Preferences</button>
      </form>
    </div>
  );
}
