using Microsoft.Extensions.Configuration;
using MysticRealms.App.ViewModel;
using System.Data.SqlClient;

namespace MysticRealms.App
{
    public static class DatabaseHelper
    {
        private static string _connectionString;

        static DatabaseHelper()
        {
            var config = ConfigurationHelper.GetConfiguration();
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public static class SessionHelper
        {
            public static string LoggedUsername { get; set; }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
        // User existence check
        public static async Task<bool> UserExists(string username)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var result = (int)await command.ExecuteScalarAsync();
                    return result > 0;
                }
            }
        }
        // User registration
        public static async Task<int> RegisterUser(string username, string email, string password)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var query = @"
                    INSERT INTO Users (Username, Email, PasswordHashed, Score, BestScore, Coins, HealthPotion)
                    VALUES (@Username, @Email, @PasswordHashed, 0, 0, 0, 0)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHashed", HashPassword(password));
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        // User validation
        public static async Task<bool> ValidateUser(string username, string password)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var query = "SELECT PasswordHashed FROM Users WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var result = await command.ExecuteScalarAsync() as string;
                    return result != null && VerifyPassword(password, result);
                }
            }
        }
        // Password hashing
        private static string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }
        // Pasword verification
        private static bool VerifyPassword(string plainPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }

        public static async Task<int> UpdateUserScore(int score)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var query = "UPDATE Users SET Score = @Score WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    string username = SessionHelper.LoggedUsername;
                    command.Parameters.AddWithValue("@Score", score);
                    command.Parameters.AddWithValue("@Username", username);
                    //command.Parameters.AddWithValue("@BestScore", bestScore);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<int> UpdateUserBestScore(int bestScore)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                var query = "UPDATE Users SET BestScore = @BestScore WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    string username = SessionHelper.LoggedUsername;
                    //command.Parameters.AddWithValue("@Score", score);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@BestScore", bestScore);
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task UpdateUserCoins(string username, int coins)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                var query = "UPDATE Users SET Coins = Coins + @Coins WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Coins", coins);
                    command.Parameters.AddWithValue("@Username", username);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public static async Task<ShowUserStats> GetUserStats()
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();

                string query = "SELECT Username, Score, BestScore, Coins, HealthPotion FROM Users WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    string username = SessionHelper.LoggedUsername; // Logged-in user's username
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ShowUserStats
                            {
                                Username = reader["Username"].ToString(),
                                Score = Convert.ToInt32(reader["Score"]),
                                BestScore = Convert.ToInt32(reader["BestScore"]),
                                Coins = Convert.ToInt32(reader["Coins"]),
                                HealthPotions = Convert.ToInt32(reader["HealthPotion"])
                            };
                        }
                    }
                }
            }

            return null; // Return null if no user found
        }

    }
}
