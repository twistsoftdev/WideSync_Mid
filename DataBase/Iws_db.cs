using Microsoft.Data.Sqlite;
using System.Xml.Linq;

namespace middleware.DataBase
{
    public interface Iws_db
    {
        List<WS_TagLog> GetTagLogs(int tagId, DateTime From, DateTime To);
    }
    public class ws_db : Iws_db
    {
        public ws_db()
        {
        }
        public List<WS_TagLog> GetTagLogs(int tagId, DateTime From, DateTime To)
        {
            List<WS_TagLog> tagLogs = new List<WS_TagLog>();
            int FromDay = int.Parse(From.ToString("yyyyMMdd"));
            int ToDay = int.Parse(To.ToString("yyyyMMdd"));

            for (int i = FromDay; i <= ToDay; i++)
            {
                string DB = i.ToString() + ".DB";
                if (!File.Exists(DB))
                {
                    continue;
                }
                SqliteConnection _connection = new SqliteConnection("Data Source=" + DB + ";");
                _connection.Open();

                string selectQuery = $"SELECT * FROM TagLog WHERE TagId = {tagId} ";
                int TimebySecondsF = (int)From.TimeOfDay.TotalSeconds;
                int TimebySecondsT = (int)From.TimeOfDay.TotalSeconds;
                if (FromDay == ToDay) // dữ liệu lấy trong cùng 1 file >> where > from and < to  
                {
                    selectQuery += $"AND Time >= {TimebySecondsF} AND Time <= {TimebySecondsT}";
                }
                else if (i == FromDay) // lấy trong ngày đầu tiên where > from  
                {
                    int TimebySeconds = (int)From.TimeOfDay.TotalSeconds;
                    selectQuery += $"AND Time >= {TimebySecondsF}";
                }
                else if (i == ToDay) // lấy trong ngày cuối cùng where < to  
                {
                    selectQuery += $"AND Time <= {TimebySecondsT}";
                }
                // lấy hết dữ liệu trong file  
                using var command = new SqliteCommand(selectQuery, _connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WS_TagLog tagLog = new WS_TagLog
                    {
                        Id = reader.GetInt32(0),
                        TagId = reader.GetInt32(1),
                        Value = reader.IsDBNull(2) ? (double?)null : reader.GetDouble(2),
                        Time = reader.GetInt32(3)
                    };
                    tagLogs.Add(tagLog);
                }
                _connection.Close();
            }
            return tagLogs;
        }
    }
}
