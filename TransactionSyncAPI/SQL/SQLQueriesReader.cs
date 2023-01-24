using Newtonsoft.Json;

namespace TransactionSyncAPI.SQL
{
    public class SQLQueriesReader
    {
        public Dictionary<string, string> Queries { get; set;}

        public SQLQueriesReader()
        {
            string json = File.ReadAllText("SQL/Queries.json");
            Queries = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
