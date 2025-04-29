using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace FilmWebAdmin
{
    public partial class AddMovieWindow : Window
    {
        private const string connectionString = "Server=127.0.0.1;Port=3306;Database=filmdatabase0420;Uid=root;Pwd=;";

        public AddMovieWindow()
        {
            InitializeComponent();
            LoadGenres();
        }

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
                    MovieGenreComboBox.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading genres: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
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

                    string query = "INSERT INTO movies (title, genre_id, release_year, img_url, favorite) VALUES (@title, @genreId, @releaseYear, @imgUrl, 0)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@genreId", genreId.HasValue ? (object)genreId.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@releaseYear", releaseYear.HasValue ? (object)releaseYear.Value.Year : DBNull.Value);
                    cmd.Parameters.AddWithValue("@imgUrl", imgUrl);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Movie '{title}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error saving movie: {ex.Message}\nError Number: {ex.Number}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving movie: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}