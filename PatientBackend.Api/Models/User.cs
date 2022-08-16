using Newtonsoft.Json;

namespace PatientBackend.Api.Models
{
    public class User
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
