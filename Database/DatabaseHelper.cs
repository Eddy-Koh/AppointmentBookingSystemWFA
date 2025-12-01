using MySql.Data.MySqlClient;

namespace AppointmentBookingSystemWFA.Database
{
    public static class DatabaseHelper
    {
        // Direct connection string
        private static readonly string connectionString = "Server=localhost;Database=appointment_db;Uid=root;Pwd=;";
        private static readonly string serverConnectionString = "Server=localhost;Uid=root;Pwd=;";

        // Ensure database and tables exist
        // Call when application startup
        public static void InitializeDatabase()
        {
            // Ensure database exists
            using (var serverConn = new MySqlConnection(serverConnectionString))
            {
                serverConn.Open();
                var createDbCmd = new MySqlCommand("CREATE DATABASE IF NOT EXISTS appointment_db", serverConn);
                createDbCmd.ExecuteNonQuery();
            }

            // Ensure tables exist
            using (var dbConn = new MySqlConnection(connectionString))
            {
                dbConn.Open();

                var createUsersTableCmd = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS users (
                        Username VARCHAR(50) PRIMARY KEY,
                        MobilePhone VARCHAR(15) NOT NULL,
                        Email VARCHAR(100) NOT NULL UNIQUE,
                        Password VARCHAR(255) NOT NULL,
                        Role VARCHAR(20) NOT NULL,
                        ApprovalStatus VARCHAR(20) NOT NULL
                    )", dbConn);
                createUsersTableCmd.ExecuteNonQuery();

                var createAppointmentsTableCmd = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS appointments (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        RequesterName VARCHAR(50) NOT NULL,
                        AppointmentDate DATE NOT NULL,
                        StartTime TIME NOT NULL,
                        EndTime TIME NOT NULL,
                        Reason TEXT,
                        Status VARCHAR(20) NOT NULL DEFAULT 'Pending'
                    )", dbConn);
                createAppointmentsTableCmd.ExecuteNonQuery();
            }
        }


        // Returns a new open connection to appointment_db
        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(connectionString);
            conn.Open();
            return conn;
        }
    }
}
