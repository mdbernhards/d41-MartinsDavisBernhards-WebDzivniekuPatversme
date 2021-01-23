using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Repository
{
    public class WebShelterDbContext
    {
        public string ConnectionString { get; set; }

        public WebShelterDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}