using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Scoreboard
{
    /// <summary>
    /// Handles connections to the Rest API endpoints and querys it for score data.
    /// </summary>
    public class ScoreboardServer
    {

        // Fields:
        private string m_sEndpoint;
        private HttpClient m_pClient;
        private JsonSerializerOptions m_pSerializationStyle;
        private ScoreDTO m_pCurrentData;

        // Properties:
        /// <summary>
        /// Gets the currently loaded data.
        /// </summary>
        public ScoreDTO ScoreData { get { return m_pCurrentData; } }

        // Constructors:
        /// <summary>
        /// Initializes the contents of the ScoreboardServer and prepares for connection.
        /// </summary>
        /// <param name="a_sEndpoint">The rest API's endpoint url.</param>
        public ScoreboardServer(string a_sEndpoint)
        {
            m_sEndpoint = a_sEndpoint;
            m_pClient = new HttpClient();
            m_pSerializationStyle = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // Methods:
        /// <summary>
        /// Connects to the Server's endpoint and gets that data.
        /// </summary>
        public async Task Get()
        {
            try
            {
                // Attempting to connect to the end point.
                HttpResponseMessage response = await m_pClient.GetAsync(m_sEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    // Loading in the json data as a string.
                    string rawJson = await response.Content.ReadAsStringAsync();

                    // Deserializing the data held by the json string.
                    m_pCurrentData = JsonSerializer.Deserialize<ScoreDTO>(
                        rawJson,                    // Raw json.
                        m_pSerializationStyle);     // Serialize style.
                }
            }
            catch (Exception error)
            {
                Debug.WriteLine($"SERVER ERROR: {error.Message}");
            }
        }
    }

    /// <summary>
    /// Data Transfer Objects to serialize the json data to.
    /// </summary>
    public class ScoreDTO
    {
        public int Box1Health { get; set; }
        public int Box2Health { get; set; }
        public int Box3Health { get; set; }
        public int Box4Health { get; set; }
        public int Box5Health { get; set; }
        public int Box6Health { get; set; }
        public int Box7Health { get; set; }
        public int Box8Health { get; set; }
        public int Box9Health { get; set; }
        public int Box10Health { get; set; }
    }
}
