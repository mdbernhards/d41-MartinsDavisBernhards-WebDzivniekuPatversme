using MySql.Data.MySqlClient;

namespace WebDzivniekuPatversme.Data
{
    public class WebShelterDbContext
    {
        public string ConnectionString { get; set; }

        public WebShelterDbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}