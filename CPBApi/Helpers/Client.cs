
using System.Text.Json.Serialization;


namespace CPBApi.Helpers
{
    public class Client
    {
        public string Id { get; set; }
        [JsonIgnore]
        public string Secret { get; set; }
    }
}
