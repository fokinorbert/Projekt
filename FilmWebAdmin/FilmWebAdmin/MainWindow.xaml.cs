using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FilmWebAdmin
{
    public partial class MainWindow : Window
    {
        private const string connectionString = "Server=127.0.0.1;Port=3306;Database=filmdatabase0420;Uid=root;Pwd=;";

        public MainWindow()
        {
            InitializeComponent();
            LoadUsers();
            LoadGenres();
            LoadMovies();
            LoadPersons();
        }

        #region Users
        private void LoadUsers()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT user_id, username, email FROM users";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    UsersListView.ItemsSource = dt.DefaultView;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedItem is DataRowView row)
            {
                UserUsernameTextBox.Text = row["username"].ToString();
                UserEmailTextBox.Text = row["email"].ToString();
                UserPasswordBox.Clear();
                UserConfirmPasswordBox.Clear();
            }
            else
            {
                UserUsernameTextBox.Clear();
                UserEmailTextBox.Clear();
                UserPasswordBox.Clear();
                UserConfirmPasswordBox.Clear();
            }
        }

        private void SaveUserButton_Click(object sender, RoutedEventArgs e)
        {
            UsersListView.SelectionChanged -= UsersListView_SelectionChanged;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string username = UserUsernameTextBox.Text.Trim();
                    string email = UserEmailTextBox.Text.Trim();
                    string password = UserPasswordBox.Password;
                    string confirmPassword = UserConfirmPasswordBox.Password;

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Username and email are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!string.IsNullOrEmpty(password) && password != confirmPassword)
                    {
                        MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    byte[] passwordHash = null;
                    byte[] passwordSalt = null;

                    if (UsersListView.SelectedItem is DataRowView row)
                    {
                        int userId = Convert.ToInt32(row["user_id"]);
                        string query = "UPDATE users SET username = @username, email = @email";
                        if (!string.IsNullOrEmpty(password))
                        {
                            Encryption.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                            query += ", password_hash = @passwordHash, password_salt = @passwordSalt";
                        }
                        query += " WHERE user_id = @userId";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        if (!string.IsNullOrEmpty(password))
                        {
                            cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                            cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                        }
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"User '{username}' updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(password))
                        {
                            MessageBox.Show("Password is required for new users.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        Encryption.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                        string query = "INSERT INTO users (username, password_hash, password_salt, email) VALUES (@username, @passwordHash, @passwordSalt, @email)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@passwordHash", passwordHash);
                        cmd.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"User '{username}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    LoadUsers();
                    UserUsernameTextBox.Clear();
                    UserEmailTextBox.Clear();
                    UserPasswordBox.Clear();
                    UserConfirmPasswordBox.Clear();
                    UsersListView.SelectedItem = null;
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Username or email already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UsersListView.SelectionChanged += UsersListView_SelectionChanged;
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListView.SelectedItem is DataRowView row)
            {
                var result = MessageBox.Show($"Are you sure you want to delete user '{row["username"]}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;

                UsersListView.SelectionChanged -= UsersListView_SelectionChanged;

                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        int userId = Convert.ToInt32(row["user_id"]);
                        string username = row["username"].ToString();
                        string query = "DELETE FROM users WHERE user_id = @userId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@userId", userId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"User '{username}' deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadUsers();
                        UserUsernameTextBox.Clear();
                        UserEmailTextBox.Clear();
                        UserPasswordBox.Clear();
                        UserConfirmPasswordBox.Clear();
                        UsersListView.SelectedItem = null;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    UsersListView.SelectionChanged += UsersListView_SelectionChanged;
                }
            }
        }
        #endregion

        #region Genres
        private void LoadGenres()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT genre_id, genre_name FROM genres";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    GenresListView.ItemsSource = dt.DefaultView;
                    MovieGenreComboBox.ItemsSource = dt.DefaultView;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading genres: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading genres: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GenresListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GenresListView.SelectedItem is DataRowView row)
            {
                GenreNameTextBox.Text = row["genre_name"].ToString();
            }
            else
            {
                GenreNameTextBox.Clear();
            }
        }

        private void AddGenreButton_Click(object sender, RoutedEventArgs e)
        {
            GenreNameTextBox.Clear();
            GenresListView.SelectedItem = null;
        }

        private void SaveGenreButton_Click(object sender, RoutedEventArgs e)
        {
            GenresListView.SelectionChanged -= GenresListView_SelectionChanged;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string genreName = GenreNameTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(genreName))
                    {
                        MessageBox.Show("Genre name is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (GenresListView.SelectedItem is DataRowView row)
                    {
                        int genreId = Convert.ToInt32(row["genre_id"]);
                        string query = "UPDATE genres SET genre_name = @genreName WHERE genre_id = @genreId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@genreName", genreName);
                        cmd.Parameters.AddWithValue("@genreId", genreId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Genre '{genreName}' updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        string query = "INSERT INTO genres (genre_name) VALUES (@genreName)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@genreName", genreName);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Genre '{genreName}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    LoadGenres();
                    LoadMovies();
                    GenreNameTextBox.Clear();
                    GenresListView.SelectedItem = null;
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Genre name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error saving genre: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving genre: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                GenresListView.SelectionChanged += GenresListView_SelectionChanged;
            }
        }

        private void DeleteGenreButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenresListView.SelectedItem is DataRowView row)
            {
                var result = MessageBox.Show($"Are you sure you want to delete genre '{row["genre_name"]}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;

                GenresListView.SelectionChanged -= GenresListView_SelectionChanged;

                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        int genreId = Convert.ToInt32(row["genre_id"]);
                        string genreName = row["genre_name"].ToString();
                        string query = "DELETE FROM genres WHERE genre_id = @genreId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@genreId", genreId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Genre '{genreName}' deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadGenres();
                        LoadMovies();
                        GenreNameTextBox.Clear();
                        GenresListView.SelectedItem = null;
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1451)
                {
                    MessageBox.Show("Cannot delete genre because it is used by movies.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error deleting genre: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting genre: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    GenresListView.SelectionChanged += GenresListView_SelectionChanged;
                }
            }
        }
        #endregion

        #region Movies
        private void LoadMovies()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT m.movie_id, m.title, g.genre_name, m.release_year
                        FROM movies m
                        LEFT JOIN genres g ON m.genre_id = g.genre_id";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    MoviesListView.ItemsSource = dt.DefaultView;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading movies: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading movies: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MoviesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MoviesListView.SelectedItem is DataRowView row)
            {
                MovieTitleTextBox.Text = row["title"].ToString();
                MovieReleaseYearPicker.SelectedDate = row["release_year"] != DBNull.Value && int.TryParse(row["release_year"].ToString(), out int year) ? new DateTime(year, 1, 1) : (DateTime?)null;
                MovieImgUrlTextBox.Text = GetMovieImgUrl(Convert.ToInt32(row["movie_id"]));

                string genreName = row["genre_name"].ToString();
                foreach (DataRowView item in MovieGenreComboBox.Items)
                {
                    if (item["genre_name"].ToString() == genreName)
                    {
                        MovieGenreComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            else
            {
                MovieTitleTextBox.Clear();
                MovieGenreComboBox.SelectedIndex = -1;
                MovieReleaseYearPicker.SelectedDate = null;
                MovieImgUrlTextBox.Clear();
            }
        }

        private string GetMovieImgUrl(int movieId)
        {
            string imgUrl = "";
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT img_url FROM movies WHERE movie_id = @movieId";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@movieId", movieId);
                    imgUrl = cmd.ExecuteScalar()?.ToString() ?? "";
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error retrieving image URL: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving image URL: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return imgUrl;
        }

        private void AddMovieButton_Click(object sender, RoutedEventArgs e)
        {
            AddMovieWindow addMovieWindow = new AddMovieWindow();
            if (addMovieWindow.ShowDialog() == true)
            {
                LoadMovies();
            }
        }

        private void SaveMovieButton_Click(object sender, RoutedEventArgs e)
        {
            MoviesListView.SelectionChanged -= MoviesListView_SelectionChanged;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string title = MovieTitleTextBox.Text.Trim();
                    int? genreId = null;
                    if (MovieGenreComboBox.SelectedItem is DataRowView genreRow)
                        genreId = Convert.ToInt32(genreRow["genre_id"]);
                    DateTime? releaseYear = MovieReleaseYearPicker.SelectedDate;
                    string imgUrl = MovieImgUrlTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(title))
                    {
                        MessageBox.Show("Title is required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (MoviesListView.SelectedItem is DataRowView row)
                    {
                        int movieId = Convert.ToInt32(row["movie_id"]);
                        string query = "UPDATE movies SET title = @title, genre_id = @genreId, release_year = @releaseYear, img_url = @imgUrl WHERE movie_id = @movieId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@genreId", genreId.HasValue ? (object)genreId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@releaseYear", releaseYear.HasValue ? (object)releaseYear.Value.Year : DBNull.Value);
                        cmd.Parameters.AddWithValue("@imgUrl", imgUrl);
                        cmd.Parameters.AddWithValue("@movieId", movieId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Movie '{title}' updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        string query = "INSERT INTO movies (title, genre_id, release_year, img_url, favorite) VALUES (@title, @genreId, @releaseYear, @imgUrl, 0)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@genreId", genreId.HasValue ? (object)genreId.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@releaseYear", releaseYear.HasValue ? (object)releaseYear.Value.Year : DBNull.Value);
                        cmd.Parameters.AddWithValue("@imgUrl", imgUrl);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Movie '{title}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    LoadMovies();
                    MovieTitleTextBox.Clear();
                    MovieGenreComboBox.SelectedIndex = -1;
                    MovieReleaseYearPicker.SelectedDate = null;
                    MovieImgUrlTextBox.Clear();
                    MoviesListView.SelectedItem = null;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error saving movie: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving movie: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                MoviesListView.SelectionChanged += MoviesListView_SelectionChanged;
            }
        }

        private void DeleteMovieButton_Click(object sender, RoutedEventArgs e)
        {
            if (MoviesListView.SelectedItem is DataRowView row)
            {
                var result = MessageBox.Show($"Are you sure you want to delete movie '{row["title"]}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;

                MoviesListView.SelectionChanged -= MoviesListView_SelectionChanged;

                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        int movieId = Convert.ToInt32(row["movie_id"]);
                        string title = row["title"].ToString();
                        string query = "DELETE FROM movies WHERE movie_id = @movieId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@movieId", movieId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Movie '{title}' deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadMovies();
                        MovieTitleTextBox.Clear();
                        MovieGenreComboBox.SelectedIndex = -1;
                        MovieReleaseYearPicker.SelectedDate = null;
                        MovieImgUrlTextBox.Clear();
                        MoviesListView.SelectedItem = null;
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1451)
                {
                    MessageBox.Show("Cannot delete movie because it is referenced by other records.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error deleting movie: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting movie: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    MoviesListView.SelectionChanged += MoviesListView_SelectionChanged;
                }
            }
        }
        #endregion

        #region Persons
        private void LoadPersons()
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT person_id, name, role, birth_date, birth_place FROM persons";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    PersonsListView.ItemsSource = dt.DefaultView;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error loading persons: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading persons: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PersonsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PersonsListView.SelectedItem is DataRowView row)
            {
                PersonNameTextBox.Text = row["name"].ToString();
                string role = row["role"].ToString();
                PersonRoleComboBox.SelectedItem = PersonRoleComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == role);
                PersonBirthDatePicker.SelectedDate = row["birth_date"] != DBNull.Value && DateTime.TryParse(row["birth_date"].ToString(), out DateTime date) ? date : (DateTime?)null;
                PersonBirthPlaceTextBox.Text = row["birth_place"].ToString();
            }
            else
            {
                PersonNameTextBox.Clear();
                PersonRoleComboBox.SelectedIndex = -1;
                PersonBirthDatePicker.SelectedDate = null;
                PersonBirthPlaceTextBox.Clear();
            }
        }

        private void SavePersonButton_Click(object sender, RoutedEventArgs e)
        {
            PersonsListView.SelectionChanged -= PersonsListView_SelectionChanged;

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string name = PersonNameTextBox.Text.Trim();
                    string role = PersonRoleComboBox.SelectedItem is ComboBoxItem selectedRole ? selectedRole.Content.ToString() : string.Empty;
                    DateTime? birthDate = PersonBirthDatePicker.SelectedDate;
                    string birthPlace = PersonBirthPlaceTextBox.Text.Trim();

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(role))
                    {
                        MessageBox.Show("Name and role are required.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (PersonsListView.SelectedItem is DataRowView row)
                    {
                        int personId = Convert.ToInt32(row["person_id"]);
                        string query = "UPDATE persons SET name = @name, role = @role, birth_date = @birthDate, birth_place = @birthPlace WHERE person_id = @personId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@birthDate", birthDate.HasValue ? (object)birthDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@birthPlace", birthPlace);
                        cmd.Parameters.AddWithValue("@personId", personId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Person '{name}' updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        string query = "INSERT INTO persons (name, role, birth_date, birth_place) VALUES (@name, @role, @birthDate, @birthPlace)";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@role", role);
                        cmd.Parameters.AddWithValue("@birthDate", birthDate.HasValue ? (object)birthDate.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@birthPlace", birthPlace);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Person '{name}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    LoadPersons();
                    PersonNameTextBox.Clear();
                    PersonRoleComboBox.SelectedIndex = -1;
                    PersonBirthDatePicker.SelectedDate = null;
                    PersonBirthPlaceTextBox.Clear();
                    PersonsListView.SelectedItem = null;
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Person name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error saving person: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving person: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                PersonsListView.SelectionChanged += PersonsListView_SelectionChanged;
            }
        }
        #endregion
        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            AddPersonWindow addPersonWindow = new AddPersonWindow();
            if (addPersonWindow.ShowDialog() == true)
            {
                LoadPersons();
            }
        }

        private void DeletePersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonsListView.SelectedItem is DataRowView row)
            {
                var result = MessageBox.Show($"Are you sure you want to delete person '{row["name"]}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                    return;

                PersonsListView.SelectionChanged -= PersonsListView_SelectionChanged;

                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        int personId = Convert.ToInt32(row["person_id"]);
                        string name = row["name"].ToString();
                        string query = "DELETE FROM persons WHERE person_id = @personId";
                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@personId", personId);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show($"Person '{name}' deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadPersons();
                        PersonNameTextBox.Clear();
                        PersonRoleComboBox.Items.Clear();
                        PersonBirthDatePicker.SelectedDate = null;
                        PersonBirthPlaceTextBox.Clear();
                        PersonsListView.SelectedItem = null;
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1451)
                {
                    MessageBox.Show("Cannot delete person because they are referenced by movies.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error deleting person: {ex.Message}\nError Number: {ex.Number}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting person: {ex.Message}\nInner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "None")}",
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    PersonsListView.SelectionChanged += PersonsListView_SelectionChanged;
                }
            }
        }
    }
}