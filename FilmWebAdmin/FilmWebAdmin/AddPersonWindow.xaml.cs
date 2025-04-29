using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FilmWebAdmin
{
    public partial class AddPersonWindow : Window
    {
        private const string connectionString = "Server=127.0.0.1;Port=3306;Database=filmdatabase0420;Uid=root;Pwd=;";

        public AddPersonWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
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

                    string query = "INSERT INTO persons (name, role, birth_date, birth_place) VALUES (@name, @role, @birthDate, @birthPlace)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@birthDate", birthDate.HasValue ? (object)birthDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@birthPlace", birthPlace);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show($"Person '{name}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("Person name already exists.", "Duplicate Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error saving person: {ex.Message}\nError Number: {ex.Number}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving person: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}