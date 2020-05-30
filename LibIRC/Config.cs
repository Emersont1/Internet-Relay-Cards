using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibIRC {
    /// <summary>
    /// Configuration for connecting to an IRC server
    /// </summary>
    public class Config {

        /// <summary>
        /// The Hostname/ IP address of the server to be connected to
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Wether or not the Connection should use SSL
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// The Username to connect with
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The User's Preffered Nickname
        /// </summary>
        /// <value></value>
        public string Nick { get; set; }

        /// <summary>
        /// The Port to connect to
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The Message to display when the client quits
        /// </summary>
        public string QuitMessage { get; set; }

        /// <summary>
        /// Default Constructor, Will not set values
        /// </summary>
        public Config () { }

        /// <summary>
        /// Converts to a JSON object
        /// </summary>
        /// <returns>A JSON representation of the object</returns>
        public String ToJson () {
            var options = new JsonSerializerOptions {
                WriteIndented = true
            };
            return JsonSerializer.Serialize<Config> (this, options);
        }

        /// <summary>
        /// Creates a Config based on a JSON Configuration
        /// </summary>
        /// <param name="Json">The Json Representation of the Configuration</param>
        /// <returns>A Configuration Object</returns>
        /// <exception cref="System.Text.Json.JsonException">Will be thrown when invalid JSON is parsed</exception>
        public static Config FromJson (String Json) {
            return JsonSerializer.Deserialize<Config> (Json);
        }
    }
}