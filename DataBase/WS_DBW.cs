using Microsoft.Data.Sqlite;
using System.Data.Common;
namespace middleware.DataBase
{
    public class WS_DBW
    {
        private SqliteConnection _connection;
        public WS_DBW(string DBNAME)
        {
            string connectionString = "Data Source=" + DBNAME + ";";
            _connection = new SqliteConnection(connectionString);
            _connection.Open();

            string createTableQuery = @"
                   CREATE TABLE IF NOT EXISTS TagLog (
                       Id INTEGER PRIMARY KEY AUTOINCREMENT,
                       TagId INTEGER NOT NULL,
                       Value REAL,
                       Time INTEGER NOT NULL
                   );";
            using var command = new SqliteCommand(createTableQuery, _connection);
            command.ExecuteNonQuery();
            string createTableQuery2 = @"
                   CREATE TABLE IF NOT EXISTS EmeterLog (
                       Id INTEGER PRIMARY KEY AUTOINCREMENT,
                       MeterID INTEGER NOT NULL,
                       U1 REAL,
                       U2 REAL,
                       U3 REAL,
                       ULN REAL,
                       ULL REAL,
                       I1 REAL,
                       I2 REAL,
                       I3 REAL,
                       IL REAL,
                       P1 REAL,
                       P2 REAL,
                       P3 REAL,
                       P REAL,
                       Q REAL,
                       S REAL,
                       PF REAL,
                       F REAL,
                       Ph REAL,
                       Qh REAL,
                       Ah REAL,
                       Time INTEGER NOT NULL
                   );";
            using var command2 = new SqliteCommand(createTableQuery2, _connection);
            command2.ExecuteNonQuery();
        }
    }
}
